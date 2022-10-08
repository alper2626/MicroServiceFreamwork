using EntityBase.Poco.Responses;
using SSTTEK.ContactInformation.Entities.Poco.ContactInformationDto;

namespace SSTTEK.ContactInformation.Business.Contracts
{
    public interface IContactInformationReportService
    {
        Task<Response<ContactInformationReportResponse>> GetLocationBasedReport();
    }
}
