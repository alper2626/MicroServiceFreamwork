using System.Reflection;
using Castle.DynamicProxy;
using CastleInterceptors.Core;
using EntityBase.Poco.Responses;
using Newtonsoft.Json;
using RedisCacheService.Contract;
using RestHelpers.DIHelpers;

namespace CastleInterceptors.Aspects.Redis
{
    public class FromRedisCacheAspect : MethodInterception
    {
        private readonly object _lock = new object();
        private string _removeKey;
        private IRedisCacheService _cacheService;
        private int _slidingMin;
        
        public FromRedisCacheAspect(string key = "", int slidingMin = 1)
        {
            _cacheService = ServiceTool.GetRootService<IRedisCacheService>();
            _slidingMin = slidingMin;
            _removeKey = key;
        }

        public async override void Intercept(IInvocation invocation)
        {
            lock (_lock)
            {
                var genericNames = invocation.GenericArguments != null ? invocation.GenericArguments.ToList() : new
                    List<Type>();

                var genericStr = genericNames.Select(q => q.Name);
                var cacheKey = string.Concat(_removeKey, ".", invocation.TargetType.FullName, ".",
                    invocation.Method.Name, string.Join('.', genericStr), "(",
                    JsonConvert.SerializeObject(invocation.Arguments), ")");

                var obj = _cacheService.Get(cacheKey);

                var foundFlag = false;
                if (!string.IsNullOrEmpty(obj))
                {
                    IResponseBase ret;
                    var instanceType = typeof(Task).IsAssignableFrom(invocation.Method.ReturnType) ? invocation.Method.ReturnType.GenericTypeArguments[0] : invocation.Method.ReturnType;

                    ret = Activator.CreateInstance(instanceType) as IResponseBase;

                    if (genericNames.Count == 0)
                    {
                        foundFlag = true;
                        invocation.ReturnValue = JsonConvert.DeserializeObject(obj, instanceType);
                    }
                    
                    if (typeof(Task).IsAssignableFrom(invocation.Method.ReturnType))
                    {
                        var metod = this.GetType().GetMethod("ConvertToGenericType").MakeGenericMethod(invocation.Method.ReturnType.GenericTypeArguments[0]);
                        invocation.ReturnValue = metod.Invoke(null, new object[] { Task.FromResult(invocation.ReturnValue) });
                    }
                }
                if (!foundFlag)
                {
                    invocation.Proceed();

                    var returnValue = typeof(Task).IsAssignableFrom(invocation.Method.ReturnType) ? Convert((invocation.ReturnValue as Task)).Result : invocation.ReturnValue;

                    var response = DynamicResponse.ToDynamicResponse(returnValue);

                    _cacheService.AddOrUpdate(cacheKey, response, _slidingMin);

                }
            }
        }

        private async Task<object> Convert(Task task)
        {
            var property = await GetTaskProperty(task);
            if (property == null)
                throw new InvalidOperationException("Task does not have a return value (" + task.GetType().ToString() + ")");
            return property.GetValue(task);
        }

        private async Task<PropertyInfo> GetTaskProperty(Task task)
        {
            await task;
            var voidTaskType = typeof(Task<>).MakeGenericType(Type.GetType("System.Threading.Tasks.VoidTaskResult"));
            if (voidTaskType.IsAssignableFrom(task.GetType()))
                throw new InvalidOperationException("Task does not have a return value (" + task.GetType().ToString() + ")");
            return task.GetType().GetProperty("Result", BindingFlags.Public | BindingFlags.Instance);
        }

        public async static Task<T> ConvertToGenericType<T>(Task<object> task)
        {
            var result = await task;

            return (T)result;
        }

    }


}
