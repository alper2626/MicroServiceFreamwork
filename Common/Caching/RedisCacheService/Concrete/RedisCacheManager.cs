using Newtonsoft.Json;
using RedisCacheService.Contract;
using RedisCacheService.Models;
using StackExchange.Redis;


namespace RedisCacheService.Concrete
{
    public class RedisCacheManager : IRedisCacheService
    {
        private readonly RedisOptions _options;

        private ConnectionMultiplexer _redis;

        public IDatabase _database { get; set; }

        public RedisCacheManager(RedisOptions options)
        {
            _options = options;

            if (_redis == null || !_redis.IsConnected)
            {
                this.Connect();
            }
        }

        public void AddOrUpdate(string key, object value, int slidingMin)
        {

            _database = this.GetDb(0);
            var data = Get(key);
            if (data != null)
                Delete(key);

            _database.StringSet(key, JsonConvert.SerializeObject(value), TimeSpan.FromMinutes(slidingMin));
        }

        public void Delete(string key)
        {
            _database = this.GetDb(0);
            _database.KeyDeleteAsync(key);
        }

        public void DeleteStartWithPattern(string pattern)
        {
            foreach (var ep in _redis.GetEndPoints())
            {
                var server = _redis.GetServer(ep);
                var keys = server.Keys(database: 0, pattern: pattern + "*").ToArray();
                if (keys.Length > 0)
                    _database.KeyDeleteAsync(keys);
            }

        }

        public string Get(string key)
        {
            _database = this.GetDb(0);
            var data = _database.StringGet(key);
            if (data.HasValue)
                return data;
            return null;

        }

        public void Connect()
        {
            var configString = $"{_options.Host}:{_options.Port}";

            _redis = ConnectionMultiplexer.Connect(configString, (opt) =>
            {
                opt.Password = _options.Password;
                opt.AbortOnConnectFail = false;
            });
        }

        private IDatabase GetDb(int db)
        {
            return _redis.GetDatabase(db);
        }
    }
}
