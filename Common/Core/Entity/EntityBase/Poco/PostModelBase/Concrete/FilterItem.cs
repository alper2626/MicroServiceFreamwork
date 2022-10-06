using EntityBase.Enum;

namespace EntityBase.Concrete
{
    /// <summary>
    /// Property value eşlemesi yapılarak listeleri filtrelemek için kullanılır.
    /// </summary>
    public class FilterItem
    {
        public string Prop { get; set; }

        public FilterOperator Operator { get; set; }

        public object Value { get; set; }

        public static FilterItem Get(string prop, FilterOperator op, object val)
        {
            return new FilterItem
            {
                Value = val,
                Prop = prop,
                Operator = op,
            };
        }
    }
}
