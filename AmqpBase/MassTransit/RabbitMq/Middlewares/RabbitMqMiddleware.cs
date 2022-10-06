using AmqpBase.Extensions;
using AmqpBase.MassTransit.RabbitMq.Consumer;
using AmqpBase.MassTransit.RabbitMq.Publisher;
using AmqpBase.MassTransit.RabbitMq.Sender;
using AmqpBase.Model;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace AmqpBase.MassTransit.RabbitMq.Middlewares
{
    public static class RabbitMqMiddleware
    {
        public static IServiceCollection AddRabbitMqModules(this IServiceCollection services,RabbitMqOptions options)
        {
            
            #region Add Consumers

            var consumers = ConsumerFinder.Find();

            services.AddMassTransit(x =>
            {
                foreach (var item in consumers)
                {
                    x.AddConsumer(item);
                }

                x.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(options.Host, options.Port, "/", host =>
                    {
                        host.Username(options.UserName);
                        host.Password(options.Password);
                    });

                    foreach (var item in consumers)
                    {
                        var instance = Activator.CreateInstance(item) as IBaseConsumer;
                        cfg.ReceiveEndpoint(instance.QueueName, e =>
                        {
                            e.ConfigureConsumer(ctx, item);
                        });
                    }
                });
            });

            services.Configure<MassTransitHostOptions>(options =>
            {
                options.WaitUntilStarted = false;
                options.StartTimeout = TimeSpan.FromSeconds(5);
                options.StopTimeout = TimeSpan.FromMinutes(1);
            });

            #endregion


            #region Dependencies Added

            services.AddScoped<IBaseSender, BaseSender>();
            services.AddScoped<IBasePublisher, BasePublisher>();

            #endregion

            return services;
        }
    }
}
