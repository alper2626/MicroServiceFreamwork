using Castle.DynamicProxy;
using CastleInterceptors.Core;
using Newtonsoft.Json;
using RedisCacheService.Contract;
using RestHelpers.DIHelpers;

namespace CastleInterceptors.Aspects.Redis
{
    public class RemoveRedisCacheAspect : MethodInterception
    {
        
        private readonly object _lock = new object();
        private IRedisCacheService _cacheService;
        private bool _removeWithPattern;
        private string _key;

        public RemoveRedisCacheAspect(string key = "", bool removeWithPattern = false)
        {
           _key = key;
           _removeWithPattern = removeWithPattern;
           _cacheService = ServiceTool.GetRootService<IRedisCacheService>();
        }

        public override void Intercept(IInvocation invocation)
        {
            lock (_lock)
            {

                if (_removeWithPattern)
                {
                    _cacheService.DeleteStartWithPattern(_key);
                }
                else
                {
                    var cacheKey = string.Concat(_key, ".", invocation.TargetType.FullName, ".", invocation.Method.Name, "(", JsonConvert.SerializeObject(invocation.Arguments), ")");
                    _cacheService.Delete(cacheKey);
                }

                invocation.Proceed();

            }
        }
    }
}
