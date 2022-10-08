using AutoMapperAdapter;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MsSqlAdapter.Repository;
using ServerBaseContract.Repository.Abstract;
using SSTTEK.Contact.AmqpService.Sender.ContactInformation;
using SSTTEK.Contact.Business.Concrete;
using SSTTEK.Contact.Business.Contract;
using SSTTEK.Contact.Business.HttpClients;
using SSTTEK.Contact.DataAccess.Concrete;
using SSTTEK.Contact.DataAccess.Contract;
using SSTTEK.Contact.Entities.Db;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestBase;
using Xunit.Microsoft.DependencyInjection;

namespace SSTTEK.Contact.Business.Tests
{
    public class ContactTestBase : XUnitBase
    {
        protected override void AddServices(IServiceCollection services, IConfiguration? configuration)
        {
            AutoMapperWrapper.Configure();
            services.AddHttpClient();
            services.AddScoped<IContactService, ContactManager>();
            services.AddScoped<IContactDal, ContactDal>();
            services.AddScoped<IQueryableRepositoryBase<ContactEntity>, MsSqlQueryableRepositoryBase<ContactEntity>>();
            services.AddScoped<IContactInformationClient, ContactInformationClient>();
            services.AddScoped<IContactInformationSender, ContactInformationSender>();

            


        }

        protected override ValueTask DisposeAsyncCore()
            => new();

        protected override IEnumerable<TestAppSettings> GetTestAppSettings()
        {
            yield return new() { Filename = "appsettings.json", IsOptional = false };
        }
    }
}
