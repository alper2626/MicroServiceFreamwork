
namespace SSTTEK.MassTransitCommon.Commands
{
    public class CreateContactInformationCommand : BaseEvent
    {
        public Guid ContactEntityId { get; set; }

        public int ContactInformationType { get; set; }

        public string Content { get; set; }
    }

}
