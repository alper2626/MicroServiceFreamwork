using AmqpBase.MassTransit.RabbitMq.Consumer;
using MassTransit;

namespace SSTTEK.ContactInformation.Consumer.Consumer.ContactInformation
{
    public class ContactInformationCreatedConsumer : BaseConsumer
    {
        public override Uri QueueUri => new Uri($"queue:{QueueName}");

        public override string QueueName => "create-contact-information-queue";

    }
}
