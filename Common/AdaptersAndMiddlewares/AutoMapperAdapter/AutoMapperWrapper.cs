using AutoMapper;
using System.Reflection;

namespace AutoMapperAdapter
{
    public class AutoMapperWrapper
    {

        private static IMapper _mapper;
        private static object _lock = new object();

        public static IMapper Mapper
        {
            get
            {
                if (_mapper == null)
                    throw new Exception("Automapper not initilezed.");
                return _mapper;
            }
        }

        private static MapperConfiguration CreateConfiguration(params Assembly[] otherDomains)
        {
            var config = new MapperConfiguration(cfg =>
            {
                var profiles = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(a => a.GetTypes().Where(type => typeof(Profile).IsAssignableFrom(type))).ToList();
                profiles.AddRange(
                otherDomains.SelectMany(a => a.GetTypes().Where(type => typeof(Profile).IsAssignableFrom(type)))
                );
                cfg.AddMaps(profiles);
            });
            return config;
        }

        public static void Configure(params Assembly[] otherDomains)
        {
            lock (_lock)
            {
                if (_mapper == null)
                    _mapper = CreateConfiguration(otherDomains).CreateMapper();
            }

        }
    }
}
