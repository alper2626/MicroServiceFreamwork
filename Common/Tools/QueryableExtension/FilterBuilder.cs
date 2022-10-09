using AutoMapperAdapter;
using EntityBase.Abstract;
using EntityBase.Concrete;
using EntityBase.Exceptions;
using System.Reflection;
using Tools.ExpressionBuilder;

namespace Tools.QueryableExtension
{
    public static partial class FilterBuilder
    {
        public static ListModel<RT> ApplyAllFilter<RT, T>(this IQueryable<T> queryable, IFilterModel model)
           where T : class, IEntity, new()
        {
            if (model == null)
            {
                throw new CustomErrorException("Filters Not Found.", 400);
            }
            var turnModel = new ListModel<RT>
            {
                CurrentPage = model.Page,
            };

            var _query = queryable.ApplyDynamicFilter(model.Filters);
            turnModel.TotalItem = _query.Count();
            turnModel.MaxPage = Math.Ceiling(turnModel.TotalItem / (decimal)model.Take);
            var query = _query.ApplyOrderFilter(model.Order, model.Desc).ApplyPageFilter(model.Skip, model.Take);
            turnModel.Items = AutoMapperWrapper.Mapper.Map<List<RT>>(query);

            return turnModel;
        }

        public static IQueryable<T> ApplyOrderFilter<T>(this IQueryable<T> queryable, string? order, bool? desc)
            where T : class, IEntity, new()
        {
            if (string.IsNullOrEmpty(order) || typeof(T).GetType().GetProperty(order) == null)
                return queryable;

            var prop = DynamicExpressions.GetPropertyGetter<T>(order);
            if (desc.HasValue && desc.Value)
                return queryable.OrderByDescending(prop);
            return queryable.OrderBy(prop);
        }

        public static IQueryable<T> ApplyPageFilter<T>(this IQueryable<T> queryable, int skip, int take)
            where T : class, IEntity, new()
        {
            if (skip == 0)
                return queryable.Take(take);
            return queryable.Skip(skip).Take(take);
        }

        public static IQueryable<T> ApplyDynamicFilter<T>(this IQueryable<T> queryable, List<FilterItem> models)
            where T : class, IEntity, new()
        {

            if (models == null)
                return queryable;

            ModelContainFiltersProperties<T>(models);
            foreach (var item in models)
            {
                if (item.Value == null)
                    continue;
                var p = DynamicExpressions.GetPredicate<T>(item.Prop, item.Operator, item.Value);
                queryable = queryable.Where(p);
            }
            return queryable;
        }

        public static void ModelContainFiltersProperties<T>(List<FilterItem> filters)
            where T : class, IEntity, new()
        {
            var props = Activator.CreateInstance<T>().GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var filter in filters)
            {
                if (!props.Any(w => w.Name == filter.Prop))
                {
                    throw new CustomErrorException($"{filter.Prop} Property Not Found.Please Try With {string.Join(", ", props.Select(w => w.Name))}", 400);
                }
            }
        }



    }
}
