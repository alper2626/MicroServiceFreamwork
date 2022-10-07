using ServerBaseContract.Repository.Abstract;
using SSTTEK.ContactInformation.Entities.Db;

namespace SSTTEK.ContactInformation.DataAccess.Contract
{
    public interface IContactInformationDal : IEntityRepositoryBase<ContactInformationEntity>
    {
    }
}
