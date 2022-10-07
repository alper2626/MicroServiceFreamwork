using AutoMapper;
using EntityBase.Concrete;
using SSTTEK.Contacts.Entities.Enum;
using SSTTEK.Contacts.Entities.Poco.ContactInformationDto;
using System.ComponentModel.DataAnnotations.Schema;

namespace SSTTEK.Contacts.Entities.Db
{
    public class ContactInformationEntity : Entity
    {
        public Guid ContactEntityId { get; set; }

        [ForeignKey("ContactEntityId")]
        public virtual ContactEntity ContactEntity { get; set; }

        public ContactInformationType ContactInformationType { get; set; }

        public string Content { get; set; }

        public string ContentIndex { get; set; }

        public bool IsRemoved { get; set; }
    }

    public class ContactInformationEntityProfile : Profile
    {
        public ContactInformationEntityProfile()
        {
            CreateMap<ContactInformationEntity, ContactInformationResponse>();

            CreateMap<ContactInformationResponse, ContactInformationEntity>()
                .ForMember(w => w.IsRemoved, q => q.Ignore());

            CreateMap<ContactInformationEntity, CreateContactInformationRequest>().ReverseMap();
        }
    }
}
