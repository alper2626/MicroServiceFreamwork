namespace SSTTEK.MassTransitCommon.Events
{
    public class LocationModifiedEvent : BaseEvent
    {
        public string OldName { get; set; }
        public string NewName { get; set; }
    }
}
