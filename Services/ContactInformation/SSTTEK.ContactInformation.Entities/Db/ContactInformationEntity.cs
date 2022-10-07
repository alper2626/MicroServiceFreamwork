using AutoMapper;
using EntityBase.Concrete;
using SSTTEK.ContactInformation.Entities.Enum;
using SSTTEK.ContactInformation.Entities.Poco.ContactInformationDto;

namespace SSTTEK.ContactInformation.Entities.Db
{
    public class ContactInformationEntity : RemovableEntity
    {
        public Guid ContactEntityId { get; set; }

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
