using FaunaDB.Client;

namespace FaunadbShopApplication.Repository
{
    
    public static  class FaunaDbClient
    {
        private static readonly string ENDPOINT = "https://db.fauna.com:443";
        public static FaunaClient GetClient(string secret) =>
            new FaunaClient(endpoint: ENDPOINT, secret: secret);
    }
}
