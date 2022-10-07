using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace SSTTEK.Contact.DataAccess.Tests.Concrete
{
    [CollectionDefinition("Dependency Injection")]
    public class ConnectionTests : ContactTestBase
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
    }
}
