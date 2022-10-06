using ServerBaseContract.Repository.Abstract;
using SSTTEK.Location.Entities.Db;

namespace SSTTEK.Location.DataAccess.Contract
{
    public interface ILocationDal : IEntityRepositoryBase<LocationEntity>
    {
    }
}
