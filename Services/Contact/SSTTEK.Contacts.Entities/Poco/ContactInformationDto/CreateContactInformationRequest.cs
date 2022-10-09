using EntityBase.Concrete;
using SSTTEK.Contact.Entities.Enum;
using System.Text.Json.Serialization;

namespace SSTTEK.Contact.Entities.Poco.ContactInformationDto
{
    public class CreateContactInformationRequest : CreateModel
    {
        [JsonIgnore]
        public Guid ContactEntityId { get; set; }

        public ContactInformationType ContactInformationType { get; set; }

        public string Content { get; set; }
    }
}
