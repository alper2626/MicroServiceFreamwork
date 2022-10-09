using AmqpBase.MassTransit.RabbitMq.Consumer;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using RestHelpers.DIHelpers;
using SSTTEK.ContactInformation.Business.Contracts;
using SSTTEK.ContactInformation.Entities.Enum;
using SSTTEK.MassTransitCommon.Commands;

namespace SSTTEK.ContactInformation.Consumer.Consumer.ContactInformation
{
    public class ContactInformationCreatedConsumer : BaseConsumer, IConsumer<CreateContactInformationCommand>
    {
        public override Uri QueueUri => new Uri($"queue:{QueueName}");

        public override string QueueName => "create-contact-information-queue";

        public async Task Consume(ConsumeContext<CreateContactInformationCommand> context)
        {
            var service = ServiceTool.ServiceProvider.GetService<IContactInformationService>();
            await service.Create(
                new Entities.Poco.ContactInformationDto.CreateContactInformationRequest {
                ContactEntityId = context.Message.ContactEntityId,
                ContactInformationType = (ContactInformationType)context.Message.ContactInformationType,
                Content = context.Message.Content
            });
            await base.Consume(context);
        } 
    }
}