using HawkSoftWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HawkSoftWebApp.Service
{
    public interface IUserServices
    {
        Task<List<User>> UserList();
        Task<User> GetUser(int userId);
        bool DeleteUser(int userId);
        Task<bool> UpdateUser(User user);
        Task<bool> InsertUser(User user);
    }
}
