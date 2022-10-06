using EntityBase.Concrete;

namespace EntityBase.Abstract
{
    /// <summary>
    /// Listeleri filtrelemek için kullanılır.
    /// </summary>
    public interface IFilterModel
    {
        /// <summary>
        /// Value Prop eşlemesi ile tablo filtrelerini tutar.
        /// </summary>
        List<FilterItem> Filters { get; set; }

        /// <summary>
        /// Order column var ise property adını tutar.
        /// </summary>
        string? Order { get; set; }

        /// <summary>
        /// Order by desc var ise true girilir.
        /// </summary>
        bool? Desc { get; set; }

        /// <summary>
        /// Kaçıncı sayfayı listeleyeceğini tutar.
        /// </summary>
        int Page { get; set; }

        /// <summary>
        /// Sayfada kaç veri olacagını tutar.
        /// </summary>
        int Take => 10;

        /// <summary>
        /// Query için skip değerini otomatik hesaplar.
        /// </summary>
        int Skip => (Page - 1) * this.Take;

    }
}
