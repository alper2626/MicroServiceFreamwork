using AutoMapperAdapter;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDbAdapter.Repository;
using ServerBaseContract.Repository.Abstract;
using SSTTEK.Location.AmqpService.Publisher.Location;
using SSTTEK.Location.Business.Concrete;
using SSTTEK.Location.Business.Contracts;
using SSTTEK.Location.DataAccess.Concrete;
using SSTTEK.Location.DataAccess.Contract;
using SSTTEK.Location.Entities.Db;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestBase;
using Xunit.Microsoft.DependencyInjection;

namespace SSTTEK.Location.Business.Tests
{
    public class LocationTestBase : XUnitBase
    {
        protected override void AddServices(IServiceCollection services, IConfiguration? configuration)
        {
            services.AddScoped<ILocationService, LocationManager>();
            AutoMapperWrapper.Configure();
            services.AddScoped<ILocationDal, LocationDal>();
            services.AddScoped<IQueryableRepositoryBase<LocationEntity>, MongoDbQueryableRepositoryBase<LocationEntity>>();
            services.AddScoped<ILocationPublisher, LocationPublisher>();
            
        }

        protected override ValueTask DisposeAsyncCore() => new();

        protected override IEnumerable<TestAppSettings> GetTestAppSettings()
        {
            yield return new() { Filename = "appsettings.json", IsOptional = false };
        }
    }
}
