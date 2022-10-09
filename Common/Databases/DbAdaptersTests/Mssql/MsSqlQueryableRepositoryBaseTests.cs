using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DbAdaptersTests.Mssql
{
    [TestCaseOrderer("Xunit.Microsoft.DependencyInjection.TestsOrder.TestPriorityOrderer", "Xunit.Microsoft.DependencyInjection")]
    public class MsSqlQueryableRepositoryBaseTests : MssqlTestBase
    {
    }
}
