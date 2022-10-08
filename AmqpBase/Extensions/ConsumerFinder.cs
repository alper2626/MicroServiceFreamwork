using AmqpBase.MassTransit.RabbitMq.Consumer;

namespace AmqpBase.Extensions
{
    public class ConsumerFinder
    {
        public static IEnumerable<Type> Find()
        {
            //Proje cok büyük değilse mantıklı büyüdüğü durumlarda direk asmyi vermek daha mantıklı olacaktır.
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
