using AutoMapper;
using EntityBase.Concrete;
using SSTTEK.Contact.Entities.Poco.ContactDto;

namespace SSTTEK.Contact.Entities.Db
{
    public class ContactEntity : RemovableEntity
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public string Firm { get; set; }
    }

    public class ContactEntityProfile : Profile
    {
        public ContactEntityProfile()
        {
            CreateMap<ContactEntity, ContactResponse>();

            CreateMap<ContactEntity, ContactDetailedResponse>()
                .ForMember(w => w.Informations, q => q.Ignore());

            CreateMap<ContactResponse, ContactEntity>()
                .ForMember(w => w.IsRemoved, q => q.Ignore());

            CreateMap<ContactEntity, CreateContactRequest>().ReverseMap();

            CreateMap<ContactEntity, UpdateContactRequest>().ReverseMap();
        }
    }
}
