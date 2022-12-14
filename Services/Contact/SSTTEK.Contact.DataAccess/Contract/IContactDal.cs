using ServerBaseContract.Repository.Abstract;
using SSTTEK.Contact.Entities.Db;

namespace SSTTEK.Contact.DataAccess.Contract
{
    public interface IContactDal : IEntityRepositoryBase<ContactEntity>
    {
    }
}
