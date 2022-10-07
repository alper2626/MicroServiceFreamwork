using EntityBase.Concrete;
using SSTTEK.Contacts.Entities.Enum;
using System.Text.Json.Serialization;

namespace SSTTEK.Contacts.Entities.Poco.ContactInformationDto
{
    public class CreateContactInformationRequest : CreateModel
    {
        public Guid ContactEntityId { get; set; }

        public ContactInformationType ContactInformationType { get; set; }

        public string Content { get; set; }

        public string ContentIndex { get; set; }

        [JsonIgnore]
        public bool IsRemoved => false;
    }
}
