using Microsoft.EntityFrameworkCore;
using MsSqlAdapter.Repository;
using ServerBaseContract;
using ServerBaseContract.Repository.Abstract;
using SSTTEK.Contact.Business.Concrete;
using SSTTEK.Contact.Business.Contract;
using SSTTEK.Contact.DataAccess.Concrete;
using SSTTEK.Contact.DataAccess.Context;
using SSTTEK.Contact.DataAccess.Contract;

namespace SSTTEK.Contact.Api.Middlewares
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

            services.AddScoped<IContactService, ContactManager>();
            services.AddScoped<IContactDal, ContactDal>();
            services.AddScoped(typeof(IQueryableRepositoryBase<>),typeof(MsSqlQueryableRepositoryBase<>));

            return services;
        }


    }
}
