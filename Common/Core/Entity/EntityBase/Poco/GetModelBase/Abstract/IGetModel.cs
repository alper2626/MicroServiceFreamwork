namespace EntityBase.Abstract
{
    /// <summary>
    /// Database den çekilen IEntityler kullanıcıya dönülürken IGetModelden kalıtılmış bir sınıfa maplenerek dönülmelidir.
    /// </summary>
    public interface IGetModel
    {
        Guid Id { get; set; }
        DateTime CreateTime { get; set; }
        DateTime UpdateTime { get; set; }
    }
}
