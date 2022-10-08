using AmqpBase.Model;
using CommonMiddlewares;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ServerBaseContract;
using SSTTEK.Contact.DataAccess.Context;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestBase;
using Xunit.Microsoft.DependencyInjection;

namespace SSTTEK.Contact.DataAccess.Tests
{
    public class ContactTestBase : XUnitBase
    {
        protected override void AddServices(IServiceCollection services, IConfiguration? configuration)
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

            services.AddTransient<DbContext, ContactModuleContext>();
        }
    
        protected override ValueTask DisposeAsyncCore()
            => new();

        protected override IEnumerable<TestAppSettings> GetTestAppSettings()
        {
            yield return new() { Filename = "appsettings.json", IsOptional = false };
        }
    }
}
