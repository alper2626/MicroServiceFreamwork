using MongoDB.Bson;
using MongoDB.Driver;
using RedisCacheService.Models;
using ServerBaseContract;
using StackExchange.Redis;
using Xunit;
using Xunit.Abstractions;

namespace SSTTEK.Location.DataAccess.Tests.Concrete
{
    [CollectionDefinition("Dependency Injection")]
    public class ConnectionTests : LocationTestBase
    {
        ITestOutputHelper _testOutputHelper;

        public ConnectionTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void ConnectDatabaseTest()
        {
            var options = GetService<DatabaseOptions>(_testOutputHelper);
            var client = new MongoClient(options.ConnectionString);
            
            var db = client.GetDatabase(options.DatabaseName);
            db.RunCommandAsync((Command<BsonDocument>)"{ping:1}")
            .Wait();

        }


        [Fact]
        public void ConnectToRedisTest()
        {
            var options = GetService<RedisOptions>(_testOutputHelper);
            var configString = $"{options.Host}:{options.Port}";

            var _redis = ConnectionMultiplexer.Connect(configString, (opt) =>
            {
                opt.Password = options.Password;
                opt.AbortOnConnectFail = false;
            });

            Assert.True(_redis.IsConnected);
        }
    }
}
