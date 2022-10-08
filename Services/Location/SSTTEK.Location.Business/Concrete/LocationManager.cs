using EntityBase.Concrete;
using EntityBase.Poco.Responses;
using SSTTEK.Location.Business.Contracts;
using SSTTEK.Location.Entities.Poco.Location;

namespace SSTTEK.Location.Business.Concrete
{
    public class LocationManager : ILocationService
    {
        public Task<Response<CreateLocationRequest>> Create(CreateLocationRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Response<LocationResponse>> Delete(FilterModel request)
        {
            throw new NotImplementedException();
        }

        public Task<Response<LocationResponse>> Get(FilterModel request)
        {
            throw new NotImplementedException();
        }

        public Task<Response<UpdateLocationRequest>> Update(UpdateLocationRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
