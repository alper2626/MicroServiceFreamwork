using AutoMapper;
using EntityBase.Concrete;
using SSTTEK.Contacts.Entities.Poco.ContactDto;

namespace SSTTEK.Contacts.Entities.Db
{
    public class ContactEntity : RemovableEntity
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public string Firm { get; set; }

        public virtual ICollection<ContactInformationEntity> ContactInformationEntities { get; set; }
    }

    public class ContactEntityProfile : Profile
    {
        public ContactEntityProfile()
        {
            CreateMap<ContactEntity, ContactResponse>();

            CreateMap<ContactResponse, ContactEntity>()
                .ForMember(w => w.IsRemoved, q => q.Ignore());

            CreateMap<ContactEntity, CreateContactRequest>().ReverseMap();

            CreateMap<ContactEntity, UpdateContactRequest>().ReverseMap();
        }
    }
}
