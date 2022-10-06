using MassTransit;

namespace AmqpBase.MassTransit.RabbitMq.Sender
{
    public interface IBaseSender
    {
        Task Send<T>(T message, Uri uriAddress)
            where T : class, new();

    }

    public class BaseSender : IBaseSender
    {
        ISendEndpointProvider _sendEndpointProvider;
        public BaseSender(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }
        public async Task Send<T>(T message, Uri uriAddress)
            where T : class, new()
        {
            var sender = await _sendEndpointProvider.GetSendEndpoint(uriAddress);
            await sender.Send(message);
        }
    }
}
