using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace RestHelpers.DIHelpers
{
    public static class ServiceTool
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        /// DİKKAT !!!!
        /// Sadece transient servis olarak döner.. 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection Create(IServiceCollection services)
        {

            ServiceProvider = services.BuildServiceProvider();

            return services;
        }

        public static T GetRootService<T>()
        {
            var httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            if (httpContextAccessor.HttpContext != null)
                return httpContextAccessor.HttpContext.RequestServices.GetRequiredService<T>();
            return ServiceProvider.GetService<T>();
        }

    }
}
