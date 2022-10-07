using EntityBase.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SSTTEK.Contacts.Entities.Poco.ContactDto
{
    public class CreateContactRequest : CreateModel
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public string Firm { get; set; }

        [JsonIgnore]
        public bool IsRemoved => false;
    }
}
