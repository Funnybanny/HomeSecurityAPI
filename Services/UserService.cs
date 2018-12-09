using HomeSecurityAPI.Interfaces;
using HomeSecurityAPI.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HomeSecurityAPI.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private MongoClient _client;
        private static IMongoDatabase _db;
        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _client = new MongoClient("mongodb://Kristof:asdlol1@ds127704.mlab.com:27704/home-security");
            _db = _client.GetDatabase("home-security");
        }

        public async Task<User> Authenticate(string username, string password)
        {
            var collection = _db.GetCollection<User>("Users");
            // needs rebuild for MongoDB
            var user = await collection.Find(x => x.Username == username && x.Password == password).SingleAsync();

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            user.Password = null;

            return user;
        }

        public async Task<User> GetbyID(int id)
        {
            var col = _db.GetCollection<User>("Users");
            return await col.Find(user => user.UserId == id).SingleAsync();
        }

        public async Task<List<User>> GetAll()
        {
            var col = _db.GetCollection<User>("Users");
            var lst = await col.Find(_ => true).ToListAsync();

            foreach (User u in lst)
            {
                u.Password = null;
            }

            return lst;
        }

        public async Task<User> Create(User u)
        {
            BsonDocument user = new BsonDocument
            {
                {"userID" , u.UserId},
                {"FirstName" , u.FirstName },
                {"LastName" , u.LastName},
                {"Username" , u.Username},
                {"Password" , u.Password}
            };

            var col = _db.GetCollection<BsonDocument>("Users");
            await col.InsertOneAsync(user);
            u.Password = null;
            return u;
        }



    }
}
