using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FaunadbShopApplication.Dto;
using FaunadbShopApplication.Repository;
using NUnit.Framework;
namespace FaunadbShopApplication.Test
{
    public class RepositoryTest: TestBase
    {
        private const string userPhoneNumber = "15417543013";
        private  IUserRepository userRepository;
        [SetUp]
        public void Setup()
        {
            userRepository  = new UserRepository(Configuration);
        }
        
        [Test]
        public async Task GetUserByPhoneTest()
        {
            var user = await userRepository.GetUserByPhone(userPhoneNumber);
            Assert.IsNotNull(user, "Can't find user in database");
        }

        [Test]
        public async Task AddUserTest()
        {
            User user = new User()
            {
                FirstName = "Ron",
                LastName = "Howard",
                BirthDate = new DateTime(1980, 1, 10),
                Password = "passw1",
                PhoneNumber = "15417543014",
                Address = "Some street 2",
                UserType = UserRoles.CUSTOMER
            };
            var result = await userRepository.AddUser(user);
            Assert.IsTrue(result, "New user hasn't been created");
        }
    }
}
