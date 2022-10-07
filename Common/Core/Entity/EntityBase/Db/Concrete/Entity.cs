using EntityBase.Abstract;
using System.ComponentModel.DataAnnotations;

namespace EntityBase.Concrete
{
    /// <summary>
    /// Tüm database modelleri IEntity veya IEntity miras alan sınıf ve arayüzlerden miras alınmalıdır.
    /// </summary>
    public class Entity : IEntity
    {
        /// <summary>
        /// Her veri için unique bir değer olmalıdır.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Verinin eklenme tarihi
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Verinin son güncellenme tarihi
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }

    public class RemovableEntity : Entity, IRemovableEntity
    {
        public bool IsRemoved { get; set; }
    }

}
