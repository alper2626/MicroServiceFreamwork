using AmqpBase.MassTransit.RabbitMq.Publisher;
using SSTTEK.MassTransitCommon.Events;

namespace SSTTEK.Location.AmqpService.Publisher.Location
{
    public class LocationPublisher : ILocationPublisher
    {
        IBasePublisher _basePublisher;

        public LocationPublisher(IBasePublisher basePublisher)
        {
            _basePublisher = basePublisher;
        }

        public Task PublishLocationModifiedEvent(LocationModifiedEvent @event)
        {
            _basePublisher.Publish(@event);
            return Task.CompletedTask;
        }
    }
}
