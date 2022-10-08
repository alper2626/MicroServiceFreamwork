using Microsoft.EntityFrameworkCore;
using ServerBaseContract;
using SSTTEK.ContactInformation.Business.Concrete;
using SSTTEK.ContactInformation.Business.Contracts;
using SSTTEK.ContactInformation.DataAccess.Concrete;
using SSTTEK.ContactInformation.DataAccess.Context;
using SSTTEK.ContactInformation.DataAccess.Contract;

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

            services.AddTransient<IContactInformationService, ContactInformationManager>();
            services.AddTransient<IContactInformationReportService, ContactInformationReportManager>();

            services.AddTransient<IContactInformationDal, ContactInformationDal>();

            return services;
        }


    }
}
