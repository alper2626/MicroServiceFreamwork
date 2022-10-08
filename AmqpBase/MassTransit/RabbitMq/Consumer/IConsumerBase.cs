using MassTransit;

namespace AmqpBase.MassTransit.RabbitMq.Consumer
{
    /// <summary>
    /// Consumer base Masstransitte vardı unuttugumuz için takla attır BaseConsumer
    /// </summary>
    public interface IBaseConsumer
    {
        Uri QueueUri { get; }
        string QueueName { get; }
    }

    public abstract class BaseConsumer : IBaseConsumer
    {
        public abstract Uri QueueUri { get; }
        public abstract string QueueName { get; }


        //Şimdilik bir iş yapılmıyor her comsume için bir işlem yapmamız gerekirse diye dursun.
        public virtual Task Consume<T>(ConsumeContext<T> context)
        where T : class, new()
        {
            return Task.CompletedTask;
        }


    }
}
