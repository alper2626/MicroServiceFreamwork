using EntityBase.Concrete;
using SSTTEK.ContactInformation.Entities.Enum;
using System.Text.Json.Serialization;

namespace SSTTEK.ContactInformation.Entities.Poco.ContactInformationDto
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
