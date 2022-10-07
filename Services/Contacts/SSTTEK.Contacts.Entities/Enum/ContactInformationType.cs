using System.ComponentModel;

namespace SSTTEK.Contacts.Entities.Enum
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
