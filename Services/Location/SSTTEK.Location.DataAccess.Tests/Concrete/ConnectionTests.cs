using AmqpBase.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using RabbitMQ.Client;
using RedisCacheService.Models;
using ServerBaseContract;
using StackExchange.Redis;
using System.Threading.Tasks;
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

        [Fact]
        public void ConnectToRabbitMq()
        {
            var options = GetScopedService<RabbitMqOptions>(_testOutputHelper);

            var exception = Record.Exception(() =>
            {

                var factory = new ConnectionFactory
                {
                    HostName = options.Host,
                    UserName = options.UserName,
                    Password = options.Password
                };
                var connection = factory.CreateConnection();
                connection.Dispose();
            });
            Assert.Null(exception);

        }
    }
}
