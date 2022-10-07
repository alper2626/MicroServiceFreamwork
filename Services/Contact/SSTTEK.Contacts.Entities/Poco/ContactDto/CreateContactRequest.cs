using EntityBase.Concrete;
using SSTTEK.Contact.Entities.Poco.ContactInformationDto;
using System.Text.Json.Serialization;

namespace SSTTEK.Contact.Entities.Poco.ContactDto
{
    public class CreateContactRequest : CreateModel
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public string Firm { get; set; }

        public List<CreateContactInformationRequest> ContactInformations { get; set; }

        [JsonIgnore]
        public bool IsRemoved => false;


    }
}
