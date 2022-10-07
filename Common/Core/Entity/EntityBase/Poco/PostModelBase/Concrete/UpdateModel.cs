using EntityBase.Abstract;
using System.Text.Json.Serialization;

namespace EntityBase.Concrete
{
    public class UpdateModel : IUpdateModel
    {
        public Guid Id { get; set; }

        [JsonIgnore]
        public DateTime CreateTime { get; set; }

        [JsonIgnore]
        public DateTime UpdateTime => DateTime.Now;
    }
}
