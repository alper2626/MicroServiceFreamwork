using AutoMapperAdapter;
using EntityBase.Abstract;
using EsterOfis.EntityBase.Abstract;
using EsterOfis.EntityBase.Concrete;
using MongoDB.Driver;
using MongoDbExtender.Models;
using ServerBaseContract;
using ServerBaseContract.Repository.Abstract;
using System.Linq.Expressions;
using Tools.QueryableExtension;

namespace MongoDbAdapter.Repository
{
    public class MongoDbQueryableRepositoryBase<T> : IQueryableRepositoryBase<T>
        where T : MongoEntity, new()
    {
        private readonly IMongoCollection<T> _collection;
        private readonly DatabaseOptions _settings;

        public MongoDbQueryableRepositoryBase(DatabaseOptions settings)
        {
            _settings = settings;
            var client = new MongoClient(_settings.ConnectionString);
            var db = client.GetDatabase(_settings.DatabaseName);
            _collection = db.GetCollection<T>(typeof(T).Name.ToLowerInvariant());
        }


        public async Task<IListModel<TMapped>> List<TMapped>(IFilterModel model, List<Expression<Func<T, object>>> joinTables = null)
        {
            var queryable = _collection.AsQueryable();
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

        public async Task<TMapped> FirstOrDefault<TMapped>(IFilterModel model, List<Expression<Func<T, object>>> joinTables = null)
        {
            var queryable = _collection.AsQueryable();
            var _query = queryable.ApplyDynamicFilter(model.Filters);
            var query = _query.ApplyOrderFilter(model.Order, model.Desc).ApplyPageFilter(model.Skip, 1);
            var res = AutoMapperWrapper.Mapper.Map<TMapped>(query.FirstOrDefault());
            return res;
        }

        public async Task<bool> AnyAsync(IFilterModel model, List<Expression<Func<T, object>>> joinTables = null)
        {
            var queryable = _collection.AsQueryable();
            var res = queryable.ApplyDynamicFilter(model.Filters).Any();
            return res;
        }

        public async Task<long> CountAsync(IFilterModel model, List<Expression<Func<T, object>>> joinTables = null)
        {
            var queryable = _collection.AsQueryable();
            var res = queryable.ApplyDynamicFilter(model.Filters).Count();
            return res;
        }

        

    }
}
