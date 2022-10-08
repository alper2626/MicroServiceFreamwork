using Castle.DynamicProxy;
using CastleInterceptors.Core;
using RedisCacheService.Contract;
using RestHelpers.DIHelpers;

namespace CastleInterceptors.Aspects.Redis
{
    public class RemoveRedisCacheAspect : MethodInterception
    {
        
        private readonly object _lock = new object();
        private IRedisCacheService _cacheService;
        private string _key;

        public RemoveRedisCacheAspect(string key = "")
        {
           _key = key;
           _cacheService = ServiceTool.GetRootService<IRedisCacheService>();
        }

        public override void Intercept(IInvocation invocation)
        {
            lock (_lock)
            {
                _cacheService.DeleteStartWithPattern(_key);
                invocation.Proceed();

            }
        }
    }
}
