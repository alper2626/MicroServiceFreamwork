using EntityBase.Abstract;

namespace EntityBase.Concrete
{
    /// <summary>
    /// Database den çekilen IEntityler kullanıcıya dönülürken IGetModelden kalıtılmış bir sınıfa maplenerek dönülmelidir.
    /// </summary>
    public class GetModel : IGetModel
    {
        public Guid Id { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}
