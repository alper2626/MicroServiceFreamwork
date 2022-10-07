using EntityBase.Concrete;
namespace SSTTEK.Contacts.Entities.Poco.ContactDto
{
    public class ContactResponse : GetModel
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public string Firm { get; set; }
    }
}
