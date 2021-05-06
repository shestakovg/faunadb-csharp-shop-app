﻿using FaunadbShopApplication.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaunadbShopApplication.Service
{
    public interface IUserService
    {
        Task<string> Authenticate(User user);
        Task<bool> AddUser(User user);
    }
}