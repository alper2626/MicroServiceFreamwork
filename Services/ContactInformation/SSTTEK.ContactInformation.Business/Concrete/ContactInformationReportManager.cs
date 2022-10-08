using EntityBase.Poco.Responses;
using SSTTEK.ContactInformation.Business.Contracts;
using SSTTEK.ContactInformation.DataAccess.Contract;
using SSTTEK.ContactInformation.Entities.Poco.ContactInformationDto;

namespace SSTTEK.ContactInformation.Business.Concrete
{
    public class ContactInformationReportManager : IContactInformationReportService
    {
        IContactInformationDal _contactInformationDal;

        public ContactInformationReportManager(IContactInformationDal contactInformationDal)
        {
            _contactInformationDal = contactInformationDal;
        }

        public async Task<Response<List<ContactInformationReportResponse>>> GetLocationBasedReport()
        {
            return Response<List<ContactInformationReportResponse>>.Success(await _contactInformationDal.GetLocationBasedReport(),200);
        }
    }
}
