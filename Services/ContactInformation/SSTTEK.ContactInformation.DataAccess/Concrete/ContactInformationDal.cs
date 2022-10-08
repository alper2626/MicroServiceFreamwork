using PostgresAdapter.Repository;
using SSTTEK.ContactInformation.DataAccess.Contract;
using SSTTEK.ContactInformation.DataAccess.Context;
using SSTTEK.ContactInformation.Entities.Db;
using SSTTEK.ContactInformation.Entities.Poco.ContactInformationDto;
using Microsoft.EntityFrameworkCore;
using ServerBaseContract;
using SSTTEK.ContactInformation.Entities.Enum;

namespace SSTTEK.ContactInformation.DataAccess.Concrete
{
    public class ContactInformationDal : PostgreSqlRepositoryBase<ContactInformationEntity, ContactInformationModuleContext>, IContactInformationDal
    {
        DatabaseOptions databaseOptions;

        public ContactInformationDal(DatabaseOptions databaseOptions)
        {
            this.databaseOptions = databaseOptions;
        }

        public async Task<List<ContactInformationReportResponse>> GetLocationBasedReport()
        {
            using (var context = new ContactInformationModuleContext(databaseOptions))
            {
                return await context.ContactInformations
                      .Where(w => w.ContactInformationType == ContactInformationType.Location && !w.IsRemoved)
                      .GroupBy(w => w.ContentIndex)
                      .Select(w => new ContactInformationReportResponse
                      {
                          Location = w.Key,
                          RegisteredContact = w.Where(w => !w.IsRemoved).Select(w => w.ContactEntityId).Count(),
                          RegisteredContactInformationPhoneNumber =
                          (
                            context.ContactInformations
                                    .Where(c => !c.IsRemoved && c.ContactInformationType == ContactInformationType.PhoneNumber && w.Any(v =>!v.IsRemoved && v.ContactEntityId == c.ContactEntityId)).Count()
                          ),
                          CreateTime = DateTime.Now,
                          UpdateTime = DateTime.Now
                      }).ToListAsync();


            }
        }
    }
}
