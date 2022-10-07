using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace CommonMiddlewares
{
    public static class CoreMiddleware
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            return services;
        }
    }
}
