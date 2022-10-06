using EntityBase.Abstract;
using System.Text.Json.Serialization;

namespace EntityBase.Concrete
{
    public class CreateModel : ICreateModel
    {
        public CreateModel()
        {
            _id = Guid.NewGuid();
        }

        private Guid _id;
        
        [JsonIgnore]
        public Guid Id
        {
            get
            {
                if (_id == Guid.Empty)
                    _id = Guid.NewGuid();
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public DateTime CreateTime => DateTime.Now;


    }
}
