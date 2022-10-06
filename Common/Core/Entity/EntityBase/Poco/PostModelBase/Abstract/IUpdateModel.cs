namespace EntityBase.Abstract
{
    /// <summary>
    /// Son kullanıcıdan veri güncellemek için alınan her model IUpdateModel veya bu arayüzden kalıtılmış bir sınıftan miras alınmalıdır.
    /// </summary>
    public interface IUpdateModel : IPostModel
    {
        DateTime CreateTime { get; set; }
        DateTime UpdateTime { get;  }
    }
}
