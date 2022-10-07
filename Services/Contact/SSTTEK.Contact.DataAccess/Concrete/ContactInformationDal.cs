﻿using MsSqlAdapter.Repository;
using SSTTEK.Contact.DataAccess.Context;
using SSTTEK.Contact.DataAccess.Contract;
using SSTTEK.Contact.Entities.Db;

namespace SSTTEK.Contact.DataAccess.Concrete
{
    public class ContactInformationDal : MsSqlRepositoryBase<ContactInformationEntity, ContactModuleContext>, IContactInformationDal
    {

        
    }
}