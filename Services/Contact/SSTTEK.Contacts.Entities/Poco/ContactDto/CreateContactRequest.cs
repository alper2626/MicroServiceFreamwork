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

        private List<CreateContactInformationRequest> contactInformations;

        public List<CreateContactInformationRequest> ContactInformations
        {
            get
            {
                return contactInformations;
            }
            set
            {
                foreach (var item in value)
                {
                    item.ContactEntityId = this.Id;
                }
                contactInformations = value;
            }
        }

        [JsonIgnore]
        public bool IsRemoved => false;


    }
}
