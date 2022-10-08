using MongoDbAdapter.Repository;
using ServerBaseContract.Repository.Abstract;
using SSTTEK.Location.AmqpService.Publisher.Location;
using SSTTEK.Location.Business.Concrete;
using SSTTEK.Location.Business.Contracts;
using SSTTEK.Location.DataAccess.Concrete;
using SSTTEK.Location.DataAccess.Contract;

namespace SSTTEK.Location.Api.Middlewares
{
    public static class LocationModuleInjectMiddleware
    {
        public static IServiceCollection InjectLocation(this IServiceCollection services)
        {
            
            services.AddTransient<ILocationService, LocationManager>();
            services.AddTransient<ILocationDal, LocationDal>();

            services.AddScoped(typeof(IQueryableRepositoryBase<>), typeof(MongoDbQueryableRepositoryBase<>));

            services.AddTransient<ILocationPublisher, LocationPublisher>();

            return services;
        }


    }
}
