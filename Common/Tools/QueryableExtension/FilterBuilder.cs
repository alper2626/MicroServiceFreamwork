using AutoMapperAdapter;
using EntityBase.Abstract;
using EntityBase.Concrete;
using EntityBase.Concrete;
using Tools.ExpressionBuilder;

namespace Tools.QueryableExtension
{
    public static partial class FilterBuilder
    {
        public static ListModel<RT> ApplyAllFilter<RT, T>(this IQueryable<T> queryable, IFilterModel model)
           where T : IEntity
        {
            var turnModel = new ListModel<RT>
            {
                CurrentPage = model.Page
            };
            var _query = queryable.ApplyDynamicFilter(model.Filters);
            turnModel.TotalItem = _query.Count();
            turnModel.MaxPage = Math.Ceiling(turnModel.TotalItem / (decimal)model.Take);
            var query = _query.ApplyOrderFilter(model.Order, model.Desc).ApplyPageFilter(model.Skip, model.Take);
            turnModel.Items = AutoMapperWrapper.Mapper.Map<List<RT>>(query);

            return turnModel;
        }

        public static IQueryable<T> ApplyOrderFilter<T>(this IQueryable<T> queryable, string? order, bool? desc)
            where T : IEntity
        {
            if (string.IsNullOrEmpty(order))
                return queryable;

            var prop = DynamicExpressions.GetPropertyGetter<T>(order);
            if (desc.HasValue && desc.Value)
                return queryable.OrderByDescending(prop);
            return queryable.OrderBy(prop);
        }

        public static IQueryable<T> ApplyPageFilter<T>(this IQueryable<T> queryable, int skip, int take)
            where T : IEntity
        {
            if (skip == 0)
                return queryable.Take(take);
            return queryable.Skip(skip).Take(take);
        }

        public static IQueryable<T> ApplyDynamicFilter<T>(this IQueryable<T> queryable, List<FilterItem> models)
            where T : IEntity
        {
            if (models == null)
                return queryable;
            foreach (var item in models)
            {
                if (item.Value == null)
                    continue;
                var p = DynamicExpressions.GetPredicate<T>(item.Prop, item.Operator, item.Value);
                queryable = queryable.Where(p);
            }
            return queryable;
        }


    }
}
