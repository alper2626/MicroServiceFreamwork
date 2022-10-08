using AmqpBase.MassTransit.RabbitMq.Sender;
using SSTTEK.MassTransitCommon.Commands;

namespace SSTTEK.Contact.AmqpService.Sender.ContactInformation
{
    /// <summary>
    /// Not : Burada da publisher kullanabilirdim. Mail adresi eklendiğinde kişiye mail gönder veya sms at gibi aksiyonlar alınabilir fakat örnek olması için command kullandım.
    /// </summary>
    public class ContactInformationSender : IContactInformationSender
    {
        IBaseSender _baseSender;

        public ContactInformationSender(IBaseSender baseSender)
        {
            _baseSender = baseSender;
        }

        public Task PublishContactInformations(CreateContactInformationCommandWrapper command)
        {
            //TODO : Alper şu string geçme işine bir çare bul. Event olursa fırlatıp geçersin ama command da bu şekilde olmamalı.
            _baseSender.Send(command,new Uri("queue:create-contact-information-queue"));
            return Task.CompletedTask;
        }
    }
}
