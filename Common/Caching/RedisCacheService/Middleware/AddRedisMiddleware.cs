using Microsoft.Extensions.DependencyInjection;
using RedisCacheService.Concrete;
using RedisCacheService.Contract;
using RedisCacheService.Models;
using Microsoft.Extensions.Options;

namespace RedisCacheService.Middleware
{
    public static class AddRedisMiddleware
    {
        public static IServiceCollection AddRedis(this IServiceCollection services, RedisOptions opt)
        {
           
            services.AddSingleton<IRedisCacheService, RedisCacheManager>();
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = $"{opt.Host}:{opt.Port}";
            });
            return services;
        }
    }
}
