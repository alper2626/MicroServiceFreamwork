using AutoMapperAdapter;
using EntityBase.Abstract;
using EsterOfis.EntityBase.Abstract;
using EsterOfis.EntityBase.Concrete;
using Microsoft.EntityFrameworkCore;
using RestHelpers.DIHelpers;
using ServerBaseContract.Repository.Abstract;
using System.Linq.Expressions;
using Tools.QueryableExtension;

namespace PostgresAdapter.Repository
{
    public class PostgreSqlQueryableRepositoryBase<T> : IQueryableRepositoryBase<T>
        where T : class, IEntity, new()
    {

        public async Task<IListModel<TMapped>> List<TMapped>(IFilterModel model, List<Expression<Func<T, object>>> joinTables = null)
        {
            using (var context = ServiceTool.GetRootService<DbContext>())
            {
                var queryable = JoinItems(context, joinTables);
                var turnModel = new ListModel<TMapped>
                {
                    CurrentPage = model.Page
                };
                var _query = queryable.ApplyDynamicFilter(model.Filters);
                turnModel.TotalItem = _query.Count();
                turnModel.MaxPage = Math.Ceiling(turnModel.TotalItem / (decimal)model.Take);
                var query = _query.ApplyOrderFilter(model.Order, model.Desc).ApplyPageFilter(model.Skip, model.Take);
                turnModel.Items = AutoMapperWrapper.Mapper.Map<List<TMapped>>(query);
                return turnModel;

            }
        }

        public async Task<TMapped> FirstOrDefault<TMapped>(IFilterModel model, List<Expression<Func<T, object>>> joinTables = null)
        {
            using (var context = ServiceTool.GetRootService<DbContext>())
            {
                var queryable = JoinItems(context, joinTables);
                var _query = queryable.ApplyDynamicFilter(model.Filters);
                var query = _query.ApplyOrderFilter(model.Order, model.Desc).ApplyPageFilter(model.Skip, 1);
                var res = AutoMapperWrapper.Mapper.Map<TMapped>(await query.FirstOrDefaultAsync());
                return res;

            }
        }

        public async Task<bool> AnyAsync(IFilterModel model, List<Expression<Func<T, object>>> joinTables = null)
        {
            using (var context = ServiceTool.GetRootService<DbContext>())
            {
                var queryable = JoinItems(context, joinTables);
                var res = await queryable.ApplyDynamicFilter(model.Filters).AnyAsync();
                return res;
            }
        }

        public async Task<long> CountAsync(IFilterModel model, List<Expression<Func<T, object>>> joinTables = null)
        {
            using (var context = ServiceTool.GetRootService<DbContext>())
            {
                var queryable = JoinItems(context, joinTables);
                var res = await queryable.ApplyDynamicFilter(model.Filters).CountAsync();
                return res;
            }
        }

        private IQueryable<T> JoinItems(DbContext context, List<Expression<Func<T, object>>> joinTables = null)
        {
            var query = context.Set<T>();
            if (joinTables != null)
            {
                foreach (var item in joinTables)
                {
                    query.Include(item);
                }
            }

            return query;
        }
    }
}
