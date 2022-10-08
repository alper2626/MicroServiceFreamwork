using CommonMiddlewares;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ServerBaseContract;
using Microsoft.Extensions.Configuration;
using TestBase;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xunit.Microsoft.DependencyInjection;
using RedisCacheService.Models;
using AmqpBase.Model;

namespace SSTTEK.Location.DataAccess.Tests
{
    public class LocationTestBase : XUnitBase
    {

        protected override void AddServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddCore();

            #region DatabaseConfigurations
            services.Configure<DatabaseOptions>(options =>
            {
                configuration.GetSection("DatabaseOptions").Bind(options);
            });
            services.AddTransient(sp =>
            {
                return sp.GetRequiredService<IOptions<DatabaseOptions>>().Value;
            });
            #endregion


            #region RedisConfigurations
            services.Configure<RedisOptions>(options =>
            {
                configuration.GetSection("RedisOptions").Bind(options);
            });
            services.AddTransient(sp =>
            {
                return sp.GetRequiredService<IOptions<RedisOptions>>().Value;
            });
            #endregion


            #region RabbitMqConfigurations

            services.Configure<RabbitMqOptions>(options =>
            {
                configuration.GetSection("RabbitMq").Bind(options);
            });
            services.AddScoped<RabbitMqOptions>(sp =>
            {
                return sp.GetRequiredService<IOptions<RabbitMqOptions>>().Value;
            });


            #endregion

        }

        protected override ValueTask DisposeAsyncCore() => new();

        protected override IEnumerable<TestAppSettings> GetTestAppSettings()
        {
            yield return new() { Filename = "appsettings.json", IsOptional = false };
        }
    }
}
