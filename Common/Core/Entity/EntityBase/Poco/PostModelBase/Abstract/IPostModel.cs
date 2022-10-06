namespace EntityBase.Abstract
{
    /// <summary>
    /// Son kullanıcıdan alınan her model IPostModel veya bu arayüzden kalıtılmış bir modelden kalıtılmalıdır.
    /// </summary>
    public interface IPostModel 
    {
        Guid Id { get; set; }
    }
}
