using AutoMapper;
using MongoDbExtender.Models;
using SSTTEK.Location.Entities.Poco.Location;

namespace SSTTEK.Location.Entities.Db
{
    public class LocationEntity : MongoEntity
    {
        public string Name { get; set; }
    }

    public class LocationEntityProfile : Profile
    {
        public LocationEntityProfile()
        {
            CreateMap<LocationEntity, CreateLocationRequest>().ReverseMap();

            CreateMap<LocationEntity, UpdateLocationRequest>().ReverseMap();

            CreateMap<LocationEntity, LocationResponse>();
        }
    }
}
