using Microsoft.EntityFrameworkCore;
using ServerBaseContract;
using SSTTEK.ContactInformation.DataAccess.Context;

namespace SSTTEK.ContactInformation.Api.Middlewares
{
    public static class ContactInformationModuleInjectMiddleware
    {
        public static IServiceCollection InjectContactInformation(this IServiceCollection services,DatabaseOptions options, bool databaseUpdateEnabled = false)
        {
            services.AddTransient<DbContext, ContactInformationModuleContext>();
            
            if (databaseUpdateEnabled)
            {
                using (var context = new ContactInformationModuleContext(options))
                {
                    context.Database.Migrate();
                }
            }

            return services;
        }


    }
}
