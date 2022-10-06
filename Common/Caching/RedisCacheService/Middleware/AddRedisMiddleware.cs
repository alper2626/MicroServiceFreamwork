using Microsoft.Extensions.DependencyInjection;
using RedisCacheService.Concrete;
using RedisCacheService.Contract;
using RedisCacheService.Models;
using Microsoft.Extensions.Options;

namespace RedisCacheService.Middleware
{
    public static class AddRedisMiddleware
    {
        public static IServiceCollection AddEsterRedis(this IServiceCollection services, RedisOptions opt)
        {
            services.AddSingleton<RedisOptions>(sp =>
            {
                return sp.GetRequiredService<IOptions<RedisOptions>>().Value;
            });

            services.AddSingleton<IRedisCacheService, RedisCacheManager>();
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = $"{opt.Host}:{opt.Port}";
            });
            return services;
        }
    }
}
