namespace EntityBase.Abstract
{
    /// <summary>
    /// Son kullanıcıdan veri oluşturmak için alınan her model ICreateModel veya bu arayüzden kalıtılmış bir sınıftan miras alınmalıdır.
    /// </summary>
    public interface ICreateModel : IPostModel
    {
        /// <summary>
        /// Verinin oluşturulma zamanını tutar.
        /// </summary>
        DateTime CreateTime { get; }
    }

}
