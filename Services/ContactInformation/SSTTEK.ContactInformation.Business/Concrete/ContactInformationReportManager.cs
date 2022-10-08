using EntityBase.Poco.Responses;
using SSTTEK.ContactInformation.Business.Contracts;
using SSTTEK.ContactInformation.Entities.Poco.ContactInformationDto;

namespace SSTTEK.ContactInformation.Business.Concrete
{
    public class ContactInformationReportManager : IContactInformationReportService
    {
        public Task<Response<ContactInformationReportResponse>> GetLocationBasedReport()
        {
            throw new NotImplementedException();
        }
    }
}
