
using SSTTEK.MassTransitCommon.Events;

namespace SSTTEK.Location.AmqpService.Publisher.Location
{
    public interface ILocationPublisher
    {
        Task PublishLocationModifiedEvent(LocationModifiedEvent @event);
    }
}
