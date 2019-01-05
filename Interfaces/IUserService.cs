using HomeSecurityAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeSecurityAPI.Interfaces
{
    public interface IUserService
    {
        Task<User> Authenticate(User u);
        Task<List<User>> GetAll();
        Task<User> GetbyUsername(string username);
        Task<User> Create(User u);
        Task<User> Update(User u, string username);
        Task Delete(string username);
    }
}
