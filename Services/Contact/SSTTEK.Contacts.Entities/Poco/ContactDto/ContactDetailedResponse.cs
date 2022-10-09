using EntityBase.Concrete;
using SSTTEK.Contact.Entities.Poco.ContactInformationDto;

namespace SSTTEK.Contact.Entities.Poco.ContactDto
{
    public class ContactDetailedResponse
    {
        public ContactResponse Contact { get; set; }

        public List<ContactInformationResponse> Informations { get; set; }
    }
}
