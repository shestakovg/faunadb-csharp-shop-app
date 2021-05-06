using FaunadbShopApplication.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaunadbShopApplication.Service
{
    public interface IUserService
    {
        bool SearchUser(User user);
        Task<string> Authenticate(User user);
        Task<bool> AddUser(User user);
    }
}
