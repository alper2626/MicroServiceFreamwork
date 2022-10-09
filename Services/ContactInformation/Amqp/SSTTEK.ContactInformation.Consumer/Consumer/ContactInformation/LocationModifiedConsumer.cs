using AmqpBase.MassTransit.RabbitMq.Consumer;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using RestHelpers.DIHelpers;
using SSTTEK.ContactInformation.Business.Contracts;
using SSTTEK.MassTransitCommon.Events;

namespace SSTTEK.ContactInformation.Consumer.Consumer.ContactInformation
{
    public class LocationModifiedConsumer : BaseConsumer,IConsumer<LocationModifiedEvent>
    {
        public override Uri QueueUri => new Uri($"queue:{QueueName}");

        public override string QueueName => "modify-location-queue";

        public async Task Consume(ConsumeContext<LocationModifiedEvent> context)
        {
            var service = ServiceTool.ServiceProvider.GetService<IContactInformationService>();
            await service.UpdateLocationNamesEventConsume(context.Message);
            await base.Consume(context);
        }
    }
}
