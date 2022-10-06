using System.ComponentModel;

namespace EntityBase.Enum
{
    /// <summary>
    /// EF kullanılamayan dbler de aynı contract ı kullanacagı entity state yerine operation type kullanıp handle edeceğiz.
    /// </summary>
    public enum OperationType
    {
        [Description("Ekleme")]
        Create = 100,
        [Description("Düzenleme")]
        Update = 200,
        [Description("Silme")]
        Delete = 300,
        [Description("Okuma")]
        Read = 400
    }

}
