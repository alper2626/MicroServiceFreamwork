using CastleInterceptors.Aspects.Redis;
using EntityBase.Poco.Responses;
using SSTTEK.ContactInformation.Entities.Poco.ContactInformationDto;

namespace SSTTEK.ContactInformation.Business.Contracts
{
    public interface IContactInformationReportService
    {
        [FromRedisCacheAspect("locationreport", 1)]
        Task<Response<List<ContactInformationReportResponse>>> GetLocationBasedReport();
    }
}
