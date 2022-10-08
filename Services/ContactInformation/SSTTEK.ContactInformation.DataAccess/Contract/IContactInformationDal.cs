using ServerBaseContract.Repository.Abstract;
using SSTTEK.ContactInformation.Entities.Db;
using SSTTEK.ContactInformation.Entities.Poco.ContactInformationDto;

namespace SSTTEK.ContactInformation.DataAccess.Contract
{
    public interface IContactInformationDal : IEntityRepositoryBase<ContactInformationEntity>
    {
        Task<List<ContactInformationReportResponse>> GetLocationBasedReport();
    }
}
