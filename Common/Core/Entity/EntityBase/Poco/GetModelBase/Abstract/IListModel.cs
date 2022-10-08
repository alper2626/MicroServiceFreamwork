
namespace EntityBase.Abstract
{
    /// <summary>
    /// Liste sayfalarında kullanılan her model IListModel veya bu arayüzü miras alan bir modelden kalıtılmalıdır.
    /// </summary>
    public interface IListModel<T>
    {
        /// <summary>
        /// Liste içerisinde bulunan elemanlar
        /// </summary>
        public IEnumerable<T> Items { get; set; }

        /// <summary>
        /// Veri toplam kaç sayfadan oluşuyor.
        /// </summary>
        public decimal MaxPage { get; set; }

        /// <summary>
        /// Tüm sayfalardaki toplam veri sayısı
        /// </summary>
        public int TotalItem { get; set; }

        /// <summary>
        /// Şuan görüntülenen sayfa numarası
        /// </summary>
        public int CurrentPage { get; set; }
    }
}
