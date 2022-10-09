using AmqpBase.MassTransit.RabbitMq.Consumer;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using RestHelpers.DIHelpers;
using SSTTEK.ContactInformation.Business.Contracts;
using SSTTEK.ContactInformation.Entities.Enum;
using SSTTEK.MassTransitCommon.Commands;

namespace SSTTEK.ContactInformation.Consumer.Consumer.ContactInformation
{
    public class ContactInformationCreatedConsumer : BaseConsumer, IConsumer<CreateContactInformationCommandWrapper>
    {
        public override Uri QueueUri => new Uri($"queue:{QueueName}");

        public override string QueueName => "create-contact-information-queue";

        public async Task Consume(ConsumeContext<CreateContactInformationCommandWrapper> context)
        {
            var service = ServiceTool.ServiceProvider.GetService<IContactInformationService>();
            var items = context.Message.Items.Select(x => new Entities.Poco.ContactInformationDto.CreateContactInformationRequest
            {
                ContactEntityId = x.ContactEntityId,
                ContactInformationType = (ContactInformationType)x.ContactInformationType,
                Content = x.Content
            });
            await service.Create(items.ToList());
            await base.Consume(context);
        } 
    }
}