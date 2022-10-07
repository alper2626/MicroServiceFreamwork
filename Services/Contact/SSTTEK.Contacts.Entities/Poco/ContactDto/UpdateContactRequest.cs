using EntityBase.Concrete;
using System.Text.Json.Serialization;

namespace SSTTEK.Contact.Entities.Poco.ContactDto
{
    public class UpdateContactRequest : UpdateModel
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public string Firm { get; set; }

        [JsonIgnore]
        public bool IsRemoved { get; set; } = false;
    }
}
