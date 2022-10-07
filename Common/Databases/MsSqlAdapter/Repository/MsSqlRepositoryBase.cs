using EntityBase.Abstract;
using EntityBase.Enum;
using Microsoft.EntityFrameworkCore;
using MsSqlAdapter.Helper;
using ServerBaseContract.Repository.Abstract;
using System.Linq.Expressions;
using System.Transactions;

namespace MsSqlAdapter.Repository
{
    public class MsSqlRepositoryBase<TEntity, TContext> : IEntityRepositoryBase<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public virtual async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>>? filter = null)
        {
            using (var context = new TContext())
            {
                return await (filter != null ? context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(filter) :
                                               context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync());
            }
        }

        public virtual async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? filter = null)
        {
            using (var context = new TContext())
            {
                return await (filter != null ? context.Set<TEntity>().AsNoTracking().Where(filter).ToListAsync() :
                                               context.Set<TEntity>().AsNoTracking().ToListAsync());
            }
        }

        public virtual TEntity? SetState(TEntity entity, OperationType state)
        {
            using (var context = new TContext())
            {
                if (state == OperationType.Remove && typeof(IRemovableEntity).IsAssignableFrom(entity.GetType()))
                {
                    (entity as IRemovableEntity).IsRemoved = true;
                }
                if (SetState(entity, state, context) == null)
                {
                    return null;
                }

                SaveChanges(entity, state, context);
                return entity;
            }
        }

        public virtual IEnumerable<TEntity>? SetState(IEnumerable<TEntity> entities, OperationType state)
        {
            using (var context = new TContext())
            {
                using (var tran = new TransactionScope())
                {
                    try
                    {
                        var removeFlag = (state == OperationType.Remove && typeof(IRemovableEntity).IsAssignableFrom(entities.First().GetType()));

                        foreach (var item in entities)
                        {
                            if (removeFlag)
                            {
                                (item as IRemovableEntity).IsRemoved = true;
                            }
                            if (SetState(item, state, context) == null)
                            {
                                tran.Dispose();
                                return null;
                            }
                            SaveChanges(item, state, context);
                        }

                        tran.Complete();
                        return entities;
                    }
                    catch (Exception ex)
                    {
                        tran.Dispose();
                        throw;
                    }
                }
            }
        }

        protected virtual IEntity? SetState(TEntity entity, OperationType state, DbContext context)
        {
            context.Entry(entity).State = state.ToEntityState();
            return entity;
        }

        protected virtual int SaveChanges(TEntity entity, OperationType state, TContext context)
        {
            if (state == OperationType.Create)
            {
                entity.CreateTime = DateTime.Now;
                entity.UpdateTime = DateTime.Now;
                context.Entry(entity).Property(w => w.CreateTime).IsModified = true;
                context.Entry(entity).Property(w => w.UpdateTime).IsModified = true;
            }
            else if (state == OperationType.Update)
            {
                entity.UpdateTime = DateTime.Now;
                context.Entry(entity).Property(w => w.UpdateTime).IsModified = true;
                context.Entry(entity).Property(w => w.CreateTime).IsModified = false;
            }
            return context.SaveChanges();
        }

    }
}
