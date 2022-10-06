using ServerBaseContract.Repository.Abstract;
using SSTTEK.Contacts.Entities.Db;

namespace SSTTEK.Contact.DataAccess.Contract
{
    public interface IContactInformationDal : IEntityRepositoryBase<ContactInformationEntity>
    {
    }
}
