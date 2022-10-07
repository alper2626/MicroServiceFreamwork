
namespace EntityBase.Abstract
{
    /// <summary>
    /// Tüm database modelleri IEntity veya IEntity miras alan sınıf ve arayüzlerden miras alınmalıdır.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Her veri için unique bir değer olmalıdır.
        /// </summary>
        Guid Id { get; set; }

        /// <summary>
        /// Verinin eklenme tarihi
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Verinin son güncellenme tarihi
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }

    public interface IRemovableEntity : IEntity
    {
        bool IsRemoved { get; set; }
    }

}
