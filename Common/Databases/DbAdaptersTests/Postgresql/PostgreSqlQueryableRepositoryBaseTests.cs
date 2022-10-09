using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DbAdaptersTests.Postgresql
{
    [TestCaseOrderer("Xunit.Microsoft.DependencyInjection.TestsOrder.TestPriorityOrderer", "Xunit.Microsoft.DependencyInjection")]
    public class PostgreSqlQueryableRepositoryBaseTests: PostgresqlTestBase
    {
    }
}
