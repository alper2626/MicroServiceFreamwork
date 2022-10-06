using EntityBase.Abstract;
using EntityBase.Enum;

namespace EntityBase.Concrete
{
    /// <summary>
    /// Listeleri filtrelemek için kullanılır.
    /// </summary>
    public class FilterModel : IFilterModel
    {
        private List<FilterItem> _filters;

        /// <summary>
        /// Value Prop eşlemesi ile tablo filtrelerini tutar.
        /// </summary>
        public List<FilterItem> Filters
        {
            get
            {
                if (_filters == null)
                    _filters = new List<FilterItem>();
                return _filters;
            }
            set
            {
                _filters = value;
            }
        }

        /// <summary>
        /// Order column var ise property adını tutar.
        /// </summary>
        public string? Order { get; set; }

        /// <summary>
        /// Order by desc var ise true girilir.
        /// </summary>
        public bool? Desc { get; set; }

        int _page;

        /// <summary>
        /// Kaçıncı sayfayı listeleyeceğini tutar.
        /// </summary>
        public int Page
        {
            get
            {
                return _page > 0 ? _page : 1;
            }
            set
            {
                _page = value;
            }
        }

        /// <summary>
        /// Sayfada kaç veri olacagını tutar.
        /// </summary>
        public int Take => 10;

        /// <summary>
        /// Query için skip değerini otomatik hesaplar.
        /// </summary>
        public int Skip => (Page - 1) * this.Take;

        public static FilterModel Get()
        {
            return new FilterModel();
        }

        public static FilterModel Get(List<FilterItem> filterItems)
        {
            return new FilterModel
            {
                Filters = filterItems,
            };
        }

        public static FilterModel Get(FilterItem filterItem)
        {
            return new FilterModel
            {
                Filters = new List<FilterItem> { filterItem },
            };
        }

        public static FilterModel Get(string prop, FilterOperator op, object val)
        {
            return new FilterModel
            {
                Filters = new List<FilterItem> { FilterItem.Get(prop, op, val != null ? val?.ToString() : null) },
            };
        }

    }
}
