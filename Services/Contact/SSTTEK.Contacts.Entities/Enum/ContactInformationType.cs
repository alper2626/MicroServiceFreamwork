using System.ComponentModel;

namespace SSTTEK.Contact.Entities.Enum
{
    public enum ContactInformationType
    {
        [Description("Phone Number")]
        PhoneNumber,
        [Description("Mail Address")]
        MailAddress,
        [Description("Location")]
        Location
    }
}
