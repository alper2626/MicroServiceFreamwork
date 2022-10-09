using AmqpBase.MassTransit.RabbitMq.Consumer;
using EntityBase.Concrete;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using RestHelpers.DIHelpers;
using SSTTEK.ContactInformation.Business.Contracts;
using SSTTEK.ContactInformation.Entities.Db;
using SSTTEK.MassTransitCommon.Commands;
using SSTTEK.MassTransitCommon.Events;

namespace SSTTEK.ContactInformation.Consumer.Consumer.ContactInformation
{
    public class ContactDeletedEventConsumer : BaseConsumer, IConsumer<ContactDeletedCommand>
    {
        public override Uri QueueUri => new Uri($"queue:{QueueName}");

        public override string QueueName => "contact-deleted-queue";

        public async Task Consume(ConsumeContext<ContactDeletedCommand> context)
        {
            var service = ServiceTool.ServiceProvider.GetService<IContactInformationService>();
            await service.Remove(FilterModel.Get(nameof(ContactInformationEntity.ContactEntityId),EntityBase.Enum.FilterOperator.Equals, context.Message.ContactEntityId));
            await base.Consume(context);
        }
    }
}
