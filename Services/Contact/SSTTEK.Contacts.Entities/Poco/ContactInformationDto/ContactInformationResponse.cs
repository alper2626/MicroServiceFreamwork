using EntityBase.Concrete;
using Extensions.EnumExtensions;
using SSTTEK.Contact.Entities.Enum;

namespace SSTTEK.Contact.Entities.Poco.ContactInformationDto
{
    public class ContactInformationResponse : GetModel
    {
        public Guid ContactEntityId { get; set; }

        public string Content { get; set; }

        public string ContentIndex { get; set; }

        public ContactInformationType ContactInformationType { get; set; }

        public string ContactInformationTypeDef => ContactInformationType.GetDescription();
    }

}
