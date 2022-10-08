using AutoMapperAdapter;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PostgresAdapter.Repository;
using ServerBaseContract.Repository.Abstract;
using SSTTEK.ContactInformation.Business.Concrete;
using SSTTEK.ContactInformation.Business.Contracts;
using SSTTEK.ContactInformation.DataAccess.Concrete;
using SSTTEK.ContactInformation.DataAccess.Contract;
using SSTTEK.ContactInformation.Entities.Db;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestBase;
using Xunit.Microsoft.DependencyInjection;

namespace SSTTEK.ContactInformation.Business.Tests
{
    public class ContactInformationTestBase : XUnitBase
    {
        protected override void AddServices(IServiceCollection services, IConfiguration? configuration)
        {
            AutoMapperWrapper.Configure();
            services.AddScoped<IContactInformationService, ContactInformationManager>();
            services.AddScoped<IContactInformationDal, ContactInformationDal>();
            services.AddScoped<IQueryableRepositoryBase<ContactInformationEntity>, PostgreSqlQueryableRepositoryBase<ContactInformationEntity>>();
        }

        protected override ValueTask DisposeAsyncCore()
            => new();

        protected override IEnumerable<TestAppSettings> GetTestAppSettings()
        {
            yield return new() { Filename = "appsettings.json", IsOptional = false };
        }
    }
}
