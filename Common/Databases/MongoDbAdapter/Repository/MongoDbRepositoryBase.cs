using EntityBase.Abstract;
using EntityBase.Enum;
using MongoDB.Driver;
using ServerBaseContract;
using ServerBaseContract.Repository.Abstract;
using System.Linq.Expressions;

namespace MongoDbAdapter.Repository
{
    public class MongoDbRepositoryBase<T> : IEntityRepositoryBase<T>
        where T : class, IEntity, new()
    {
        private readonly IMongoCollection<T> _collection;
        private readonly DatabaseOptions _settings;

        public MongoDbRepositoryBase(DatabaseOptions settings)
        {
            _settings = settings;
            var client = new MongoClient(_settings.ConnectionString);
            var db = client.GetDatabase(_settings.DatabaseName);
            _collection = db.GetCollection<T>(typeof(T).Name.ToLowerInvariant());
        }

        public virtual async Task<T?> GetAsync(Expression<Func<T, bool>>? filter = null)
        {
            return await _collection.Find(filter).FirstOrDefaultAsync();

        }

        public Task<List<T>> GetListAsync(Expression<Func<T, bool>>? filter = null)
        {
            var data = filter == null
                ? _collection.AsQueryable().ToList()
                : _collection.AsQueryable().Where(filter).ToList();

            return Task.FromResult(data);
        }

        public T? SetState(T entity, OperationType state)
        {
            switch (state)
            {
                case OperationType.Create:
                    return Add(entity).Result;
                case OperationType.Update:
                    return Update(entity).Result;
                case OperationType.Delete:
                    return Delete(entity).Result;
                default:
                    throw new MissingMethodException(nameof(state));
            }
        }

        public IEnumerable<T>? SetState(IEnumerable<T> entities, OperationType state)
        {
            var turnList = new List<T>();
            switch (state)
            {
                case OperationType.Create:
                    foreach (var item in entities)
                    {
                        turnList.Add(this.Add(item).Result);
                    }
                    break;
                case OperationType.Update:
                    foreach (var item in entities)
                    {
                        turnList.Add(this.Update(item).Result);
                    }
                    break;
                case OperationType.Delete:
                    foreach (var item in entities)
                    {
                        turnList.Add(this.Delete(item).Result);
                    }
                    break;
                default:
                    throw new MissingMethodException(nameof(state));
            }
            return turnList;
        }

        private async Task<T> Add(T entity)
        {
            entity.CreateTime = DateTime.Now;
            entity.UpdateTime = DateTime.Now;
            var options = new InsertOneOptions { BypassDocumentValidation = false };
            await _collection.InsertOneAsync(entity, options);
            return entity;
        }

        private async Task<T> Update(T entity)
        {
            entity.UpdateTime = DateTime.Now;
            return await _collection.FindOneAndReplaceAsync(x => x.Id == entity.Id, entity);
        }

        private async Task<T> Delete(T entity)
        {
            return await _collection.FindOneAndDeleteAsync(x => x.Id == entity.Id);
        }
    }
}
