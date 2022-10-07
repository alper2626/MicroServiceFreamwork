using AutoMapper;
using EntityBase.Concrete;
using SSTTEK.Contact.Entities.Enum;
using SSTTEK.Contact.Entities.Poco.ContactInformationDto;
using System.ComponentModel.DataAnnotations.Schema;

namespace SSTTEK.Contact.Entities.Db
{
    public class ContactInformationEntity : RemovableEntity
    {
        public Guid ContactEntityId { get; set; }

        [ForeignKey("ContactEntityId")]
        public virtual ContactEntity ContactEntity { get; set; }

        public ContactInformationType ContactInformationType { get; set; }

        public string Content { get; set; }

        public string ContentIndex { get; set; }

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
