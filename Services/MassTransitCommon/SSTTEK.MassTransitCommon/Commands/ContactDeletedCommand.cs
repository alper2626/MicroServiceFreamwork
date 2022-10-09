namespace SSTTEK.MassTransitCommon.Commands
{
    public class ContactDeletedCommand : BaseEvent
    {
        public Guid ContactEntityId { get; set; }
    }
}
