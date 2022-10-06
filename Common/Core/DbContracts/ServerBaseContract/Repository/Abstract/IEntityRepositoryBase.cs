using EntityBase.Abstract;
using EntityBase.Enum;
using System.Linq.Expressions;

namespace ServerBaseContract.Repository.Abstract
{
    public interface IEntityRepositoryBase<T>
        where T : class, IEntity, new()
    {
        Task<T?> GetAsync(Expression<Func<T, bool>>? filter = null);

        Task<List<T>> GetListAsync(Expression<Func<T, bool>>? filter = null);

        T? SetState(T entity, OperationType state);

        IEnumerable<T>? SetState(IEnumerable<T> entities, OperationType state);
    }
}
