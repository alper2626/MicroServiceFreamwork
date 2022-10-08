using EntityBase.Abstract;
using System.Linq.Expressions;

namespace ServerBaseContract.Repository.Abstract
{
    public interface IQueryableRepositoryBase<T>
        where T : class, IEntity, new()
    {
        Task<IListModel<TMapped>> List<TMapped>(IFilterModel model, List<Expression<Func<T, object>>> joinTables = null);

        Task<TMapped> FirstOrDefault<TMapped>(IFilterModel model, List<Expression<Func<T, object>>> joinTables = null);

        Task<bool> AnyAsync(IFilterModel model, List<Expression<Func<T, object>>> joinTables = null);

        Task<long> CountAsync(IFilterModel model, List<Expression<Func<T, object>>> joinTables = null);
    }
}
