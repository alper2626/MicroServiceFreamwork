using AmqpBase.MassTransit.RabbitMq.Consumer;
using System.Reflection;

namespace AmqpBase.Extensions
{
    public class ConsumerFinder
    {
        public static IEnumerable<Type> Find(Assembly assembly = null)
        {
            if (assembly == null)
            {
                return Enumerable.Empty<Type>();
            }

            List<Type> types = new List<Type>();
            foreach (Type type in
                assembly.GetTypes().Where(type => typeof(IBaseConsumer).IsAssignableFrom(type) && !type.IsAbstract))
            {
                types.Add(type);
            }
            return types;
        }
    }
}
