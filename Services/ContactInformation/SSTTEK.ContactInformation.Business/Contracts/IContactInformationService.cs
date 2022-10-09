using CastleInterceptors.Aspects.Redis;
using EntityBase.Concrete;
using EntityBase.Poco.Responses;
using SSTTEK.ContactInformation.Entities.Poco.ContactInformationDto;
using SSTTEK.MassTransitCommon.Events;

namespace SSTTEK.ContactInformation.Business.Contracts
{
    public interface IContactInformationService
    {
        [RemoveRedisCacheAspect("locationreport")]
        Task<Response<CreateContactInformationRequest>> Create(CreateContactInformationRequest request);

        Task<Response<List<CreateContactInformationRequest>>> Create(List<CreateContactInformationRequest> request);

        Task<Response<List<ContactInformationResponse>>> GetList(FilterModel request);

        Task<Response<ContactInformationResponse>> Get(FilterModel request);

        [RemoveRedisCacheAspect("locationreport")]
        Task<Response<List<ContactInformationResponse>>> Remove(FilterModel request);

        Task UpdateLocationNamesEventConsume(LocationModifiedEvent @event);
    }
}
