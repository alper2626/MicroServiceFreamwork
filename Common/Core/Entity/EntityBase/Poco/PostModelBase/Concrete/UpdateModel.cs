using EntityBase.Abstract;

namespace EntityBase.Concrete
{
    public class UpdateModel : IUpdateModel
    {
        public Guid Id { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime => DateTime.Now;
    }
}
