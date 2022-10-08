using AmqpBase.MassTransit.RabbitMq.Consumer;

namespace SSTTEK.ContactInformation.Consumer.Consumer.ContactInformation
{
    public class LocationModifiedConsumer : BaseConsumer
    {
        public override Uri QueueUri => new Uri($"queue:{QueueName}");

        public override string QueueName => "modify-location-queue";
    }
}
