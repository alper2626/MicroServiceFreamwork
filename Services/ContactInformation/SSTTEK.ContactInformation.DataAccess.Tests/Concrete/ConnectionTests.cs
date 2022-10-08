using AmqpBase.Model;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using Xunit;
using Xunit.Abstractions;

namespace SSTTEK.ContactInformation.DataAccess.Tests.Concrete
{
    [CollectionDefinition("Dependency Injection")]
    public class ConnectionTests : ContactInformationTestBase
    {
        ITestOutputHelper _testOutputHelper;

        public ConnectionTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void ConnectDatabaseTest()
        {
            var connectable = false;
            var exception = Record.Exception(() =>
             {
                 using (var context = GetService<DbContext>(_testOutputHelper))
                 {
                     connectable = context.Database.CanConnect();
                 }
             });

            Assert.True(connectable);
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
