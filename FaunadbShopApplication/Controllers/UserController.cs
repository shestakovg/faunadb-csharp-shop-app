using FaunadbShopApplication.Dto;
using FaunadbShopApplication.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaunadbShopApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("authenticate")]
        public async Task<string> Authentificate(User user)
        {
            return await _userService.Authenticate(user);
        }
        
        [Authorize]
        [HttpPost("adduser")]
        public async Task<bool> AddUser(User user)
        {
            return await _userService.AddUser(user);
        }
    }
}
