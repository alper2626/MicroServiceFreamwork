using System.ComponentModel;

namespace SSTTEK.ContactInformation.Entities.Enum
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
