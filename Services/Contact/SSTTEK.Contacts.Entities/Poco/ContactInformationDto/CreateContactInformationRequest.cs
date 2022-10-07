using EntityBase.Concrete;
using SSTTEK.Contact.Entities.Enum;
using System.Text.Json.Serialization;

namespace SSTTEK.Contact.Entities.Poco.ContactInformationDto
{
    public class CreateContactInformationRequest : CreateModel
    {
        public Guid ContactEntityId { get; set; }

        public ContactInformationType ContactInformationType { get; set; }

        public string Content { get; set; }

        [JsonIgnore]
        public string ContentIndex => Content.ToLower();

        [JsonIgnore]
        public bool IsRemoved => false;
    }
}
