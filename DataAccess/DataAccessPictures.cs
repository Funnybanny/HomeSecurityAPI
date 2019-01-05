using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeSecurityAPI.Models;

namespace HomeSecurityAPI.DataAccess
{

    public class DataAccessPictures
    {
        MongoClient _client;
        static IMongoDatabase _db;
        public DataAccessPictures()
        {
            _client = new MongoClient("mongodb://Kristof:asdlol1@ds127704.mlab.com:27704/home-security");
            _db = _client.GetDatabase("home-security");
        }

        public async Task<Picture> Create(Picture p)
        {
            BsonDocument picture = new BsonDocument {
                {"userID" , p.userID },
                {"Base64" , p.Base64},
                {"Timestamp" , DateTime.Now }
            };
            var collection = _db.GetCollection<BsonDocument>("Pictures");
            await collection.InsertOneAsync(picture);
            return p;
        }

        public async Task<List<Picture>> GetAllbyUserID(int id)
        {
            var col = _db.GetCollection<Picture>("Pictures");
            return await col.Find(pic => pic.userID == id).ToListAsync();
            
        }

        public async Task Delete(string objId)
        {
            var col = _db.GetCollection<Picture>("Pictures");
            await col.DeleteOneAsync(p => p.Id == ObjectId.Parse(objId));
        }

        public async Task<List<Picture>> GetPictureByDate(DateTime date)
        {
            var col = _db.GetCollection<Picture>("Pictures");
            return await col.Find(pic => pic.Timestamp == date).ToListAsync();
        }

        public async Task<List<Picture>> GetPictureByIntervallum(DateTime date1, DateTime date2)
        {
            var col = _db.GetCollection<Picture>("Pictures");
            return await col.Find(pic => 
                    pic.Timestamp >= date1 
                    && 
                    pic.Timestamp <= date2).ToListAsync();
        }
        //we need pictures within certain time frames
    }
}
