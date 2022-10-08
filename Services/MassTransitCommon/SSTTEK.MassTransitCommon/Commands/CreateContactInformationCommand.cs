
using AutoMapper;
using SSTTEK.Contact.Entities.Poco.ContactInformationDto;

namespace SSTTEK.MassTransitCommon.Commands
{
    public class CreateContactInformationCommand : BaseEvent
    {
        public Guid ContactEntityId { get; set; }

        public int ContactInformationType { get; set; }

        public string Content { get; set; }
    }

    public class CreateContactInformationCommandWrapper
    {
        public List<CreateContactInformationCommand> Items { get; set; }
    }

    public class CreateContactInformationCommandProfile : Profile
    {
        public CreateContactInformationCommandProfile()
        {
            CreateMap<CreateContactInformationRequest, CreateContactInformationCommand>()
                .ForMember(w => w.EventCreatedTime, q => q.Ignore())
                .ForMember(w => w.EventOwner, q => q.Ignore())
                .ForMember(w => w.ContactInformationType, q => q.MapFrom(x => (int)x.ContactInformationType));
        }
    }

}
