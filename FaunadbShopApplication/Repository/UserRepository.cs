using FaunadbShopApplication.Dto;
using Microsoft.Extensions.Configuration;
using System;
using FaunaDB.Client;
using FaunaDB.Types;
using System.Threading.Tasks;
using static FaunaDB.Query.Language;

using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.CodeAnalysis;

namespace FaunadbShopApplication.Repository
{
    public class UserRepository : IUserRepository
    {
        private  IConfiguration Configuration { get; }
        private const string COLLECTION_NAME = "Users";
        private const string SEARCH_BY_PHONE_INDEX = "User_by_phone";

        public UserRepository(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public async Task<bool> AddUser([NotNull]User user)
        {
            var client = GetClient();
            var searchResult =  await GetUserByPhone(user.PhoneNumber);
            
            if (searchResult != null)
            {
                throw new ArgumentException($"User already exists. Phone number: {user.PhoneNumber}");
            }

            await client.Query(
                       Create(
                               Collection(COLLECTION_NAME),
                               Obj("data", Encoder.Encode(user))
                           )
                   );
            return true;
        }

        public async Task<User> GetUserByPhone(string phoneNumber)
        {
            if (String.IsNullOrEmpty(phoneNumber))
            {
                throw new ArgumentNullException(nameof(phoneNumber));
            }

            var client = GetClient();
            var values = await client.Query(Map(
                        Paginate(
                                    Match(Index(SEARCH_BY_PHONE_INDEX), phoneNumber)
                                ),
                                Lambda("userRef", Get(Var("userRef")))
                    )
                );
            IResult<Value[]> data = values.At("data").To<Value[]>();
            if (data.Value.Length == 0)
            {
                return null;
            }
           
            User user = Decoder.Decode<User>(data.Value.First().At("data"));
            user.IdRef = (data.Value.First().At("ref") as RefV).Id;
            return user;
        }

        private FaunaClient GetClient() 
        {
            var adminKey = Configuration["FaunaSettings:ADMIN_KEY"];
            return FaunaDbClient.GetClient(adminKey);
        }
    }
}
