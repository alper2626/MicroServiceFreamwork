using EntityBase.Concrete;
using EntityBase.Poco.Responses;
using SSTTEK.Location.Entities.Poco.Location;

namespace SSTTEK.Location.Business.Contracts
{
    public interface ILocationService
    {
        Task<Response<CreateLocationRequest>> Create(CreateLocationRequest request);

        Task<Response<UpdateLocationRequest>> Update(UpdateLocationRequest request);

        Task<Response<IEnumerable<LocationResponse>>> GetList();

        Task<Response<LocationResponse>> Delete(Guid id);
    }
}
