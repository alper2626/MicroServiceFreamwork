using Microsoft.EntityFrameworkCore;
using ServerBaseContract;
using SSTTEK.Contact.DataAccess.Context;

namespace SSTTEK.ContactsApi.Middlewares
{
    public static class ContactModuleInjectMiddleware
    {
        public static IServiceCollection InjectContact(this IServiceCollection services,DatabaseOptions options, bool databaseUpdateEnabled = false)
        {
            services.AddTransient<DbContext, ContactModuleContext>();

            if (databaseUpdateEnabled)
            {
                using (var context = new ContactModuleContext(options))
                {
                    context.Database.Migrate();
                }
            }

            return services;
        }


    }
}
