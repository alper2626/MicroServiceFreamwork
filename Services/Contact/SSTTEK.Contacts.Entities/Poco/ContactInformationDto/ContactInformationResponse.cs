using EntityBase.Concrete;

namespace SSTTEK.Contact.Entities.Poco.ContactInformationDto
{
    public class ContactInformationResponse : GetModel
    {
        public Guid ContactEntityId { get; set; }

        public string Content { get; set; }

        public string ContactInformationType { get; set; }
    }
}
