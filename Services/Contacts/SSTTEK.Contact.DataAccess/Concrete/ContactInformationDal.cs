using MsSqlAdapter.Repository;
using SSTTEK.Contact.DataAccess.Context;
using SSTTEK.Contact.DataAccess.Contract;
using SSTTEK.Contacts.Entities.Db;

namespace SSTTEK.Contact.DataAccess.Concrete
{
    public class ContactInformationDal : MsSqlRepositoryBase<ContactInformationEntity, ContactModuleContext>, IContactInformationDal
    {

        
    }
}
