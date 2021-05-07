using FaunadbShopApplication.Dto;
using FaunadbShopApplication.Repository;
using FaunadbShopApplication.Service;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaunadbShopApplication.Test
{
    public class ServiceTest: TestBase
    {
        private IUserRepository userRepository;
        private IUserService userService;
        
        [SetUp]
        public void Setup()
        {
            userRepository = new UserRepository(Configuration);
            userService = new UserService(userRepository, Configuration);
        }

        [Test]
        public async Task AuthenticationTest()
        {
            User user = new User()
            {
                Password = "",
                PhoneNumber = "15417543013"
            };
            AuthentificatedUser token = await userService.Authenticate(user);
            Assert.IsNotNull(token, "Can't get token");
        }
    }
}
