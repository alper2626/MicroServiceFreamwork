using PostgresAdapter.Repository;
using SSTTEK.ContactInformation.DataAccess.Contract;
using SSTTEK.ContactInformation.DataAccess.Context;
using SSTTEK.ContactInformation.Entities.Db;

namespace SSTTEK.ContactInformation.DataAccess.Concrete
{
    public class ContactInformationDal : PostgreSqlRepositoryBase<ContactInformationEntity, ContactInformationModuleContext>, IContactInformationDal
    {

        
    }
}
