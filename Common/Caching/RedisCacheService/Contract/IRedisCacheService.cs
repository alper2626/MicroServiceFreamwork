
namespace RedisCacheService.Contract
{
    public interface IRedisCacheService
    {
        string Get(string key);

        void AddOrUpdate(string key, object value, int slidingMin);

        void Delete(string key);

        void DeleteStartWithPattern(string pattern);

        void Connect();
    }
}
