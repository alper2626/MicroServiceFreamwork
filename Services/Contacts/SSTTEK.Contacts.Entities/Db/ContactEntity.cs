using EntityBase.Concrete;

namespace SSTTEK.Contacts.Entities.Db
{
    public class ContactEntity : Entity
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public string Firm { get; set; }

        public virtual ICollection<ContactInformationEntity> ContactInformationEntities { get; set; }
    }
}
