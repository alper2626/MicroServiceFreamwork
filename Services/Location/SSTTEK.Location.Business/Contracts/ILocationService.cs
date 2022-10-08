using EntityBase.Concrete;
using EntityBase.Poco.Responses;
using SSTTEK.Location.Entities.Poco.Location;

namespace SSTTEK.Location.Business.Contracts
{
    public interface ILocationService
    {
        Task<Response<CreateLocationRequest>> Create(CreateLocationRequest request);

        Task<Response<UpdateLocationRequest>> Update(UpdateLocationRequest request);

        Task<Response<IEnumerable<LocationResponse>>> GetList(FilterModel request);

        Task<Response<LocationResponse>> Get(FilterModel request);

        Task<Response<IEnumerable<LocationResponse>>> Delete(FilterModel request);
    }
}
