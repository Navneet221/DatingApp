using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dating_Api.Models;

namespace Dating_Api.Data
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string username, string password);

        Task<bool> UserExist(string username);
    }
}
