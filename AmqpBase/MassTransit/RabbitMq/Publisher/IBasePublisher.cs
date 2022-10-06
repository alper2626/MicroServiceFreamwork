using MassTransit;

namespace AmqpBase.MassTransit.RabbitMq.Publisher
{
    public interface IBasePublisher
    {
        Task Publish<T>(T message)
           where T : class, new();
    }

    public class BasePublisher : IBasePublisher
    {
        IPublishEndpoint _publishEndpoint;

        public BasePublisher(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task Publish<T>(T message)
            where T : class, new()
        {
            await _publishEndpoint.Publish(message);
        }
    }
}
