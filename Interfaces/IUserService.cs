using HomeSecurityAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeSecurityAPI.Interfaces
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
        Task<List<User>> GetAll();
        Task<User> GetbyID(int id);
        Task<User> Create(User u);
    }
}
