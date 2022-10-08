using CastleInterceptors.Aspects.Redis;
using EntityBase.Concrete;
using EntityBase.Poco.Responses;
using SSTTEK.ContactInformation.Entities.Poco.ContactInformationDto;

namespace SSTTEK.ContactInformation.Business.Contracts
{
    public interface IContactInformationService
    {
        [RemoveRedisCacheAspect("locationreport")]
        Task<Response<CreateContactInformationRequest>> Create(CreateContactInformationRequest request);

        Task<Response<IEnumerable<ContactInformationResponse>>> GetList(FilterModel request);

        Task<Response<ContactInformationResponse>> Get(FilterModel request);

        [RemoveRedisCacheAspect("locationreport")]
        Task<Response<IEnumerable<ContactInformationResponse>>> Remove(FilterModel request);
    }
}
