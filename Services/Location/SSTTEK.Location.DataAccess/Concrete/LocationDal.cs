using MongoDbAdapter.Repository;
using ServerBaseContract;
using SSTTEK.Location.DataAccess.Contract;
using SSTTEK.Location.Entities.Db;

namespace SSTTEK.Location.DataAccess.Concrete
{
    public class LocationDal : MongoDbRepositoryBase<LocationEntity>, ILocationDal
    {
        public LocationDal(DatabaseOptions settings) : base(settings)
        {
        }
    }
}
