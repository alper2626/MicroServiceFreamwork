using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDbAdapter.Repository;
using MsSqlAdapter.Repository;
using PostgresAdapter.Repository;
using ServerBaseContract.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBase;
using Xunit.Microsoft.DependencyInjection;

namespace DbAdaptersTests
{
    public class DatabaseTestBase : XUnitBase
    {

        protected override void AddServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<DbContext, EfTestContext>();
        }

        protected override ValueTask DisposeAsyncCore() => new();

        protected override IEnumerable<TestAppSettings> GetTestAppSettings()
        {
            yield return new() { Filename = "appsettings.json", IsOptional = false };
        }
    }

    public class MssqlTestBase : DatabaseTestBase
    {
        protected override void AddServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IQueryableRepositoryBase<TestEntity>>(sp =>
            {
                return new MsSqlQueryableRepositoryBase<TestEntity>();
            });
            services.AddTransient<IEntityRepositoryBase<TestEntity>>(sp =>
            {
                return new MsSqlRepositoryBase<TestEntity, EfTestContext>();
            });
            base.AddServices(services, configuration);
        }
    }

    public class PostgresqlTestBase : DatabaseTestBase
    {
        protected override void AddServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IQueryableRepositoryBase<TestEntity>>(sp =>
            {
                return new PostgreSqlQueryableRepositoryBase<TestEntity>();
            });
            services.AddTransient<IEntityRepositoryBase<TestEntity>>(sp =>
            {
                return new PostgreSqlRepositoryBase<TestEntity, EfTestContext>();
            });
            base.AddServices(services, configuration);
        }
    }

    public class MongoDbTestBase : DatabaseTestBase
    {
        protected override void AddServices(IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddTransient<IEntityRepositoryBase<TestMongoEntity>>(sp =>
            {
                return new MongoDbRepositoryBase<TestMongoEntity>(new ServerBaseContract.DatabaseOptions()
                {
                    ConnectionString = configuration["DatabaseOptions:ConnectionString"],
                    DatabaseName = configuration["DatabaseOptions:DatabaseName"],
                });
            });
            base.AddServices(services, configuration);
        }
    }
}
