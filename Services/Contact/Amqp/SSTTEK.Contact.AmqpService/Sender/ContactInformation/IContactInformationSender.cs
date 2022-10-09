using SSTTEK.MassTransitCommon.Commands;

namespace SSTTEK.Contact.AmqpService.Sender.ContactInformation
{
    public interface IContactInformationSender
    {
        Task PublishContactInformations(CreateContactInformationCommandWrapper command);

        Task PublishContactDeletedCommand(ContactDeletedCommand command);
    }
}
