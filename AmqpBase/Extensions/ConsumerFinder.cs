using AmqpBase.MassTransit.RabbitMq.Consumer;

namespace AmqpBase.Extensions
{
    public class ConsumerFinder
    {
        public static IEnumerable<Type> Find()
        {
            List<Type> types = new List<Type>();
            foreach (Type type in
                AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(a => a.GetTypes().Where(type => typeof(IBaseConsumer).IsAssignableFrom(type) && !type.IsAbstract)))
            {
                types.Add(type);
            }
            return types;
        }
    }
}
