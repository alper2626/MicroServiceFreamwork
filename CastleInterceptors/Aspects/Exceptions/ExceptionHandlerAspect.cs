using Castle.DynamicProxy;
using CastleInterceptors.Core;
using EntityBase.Poco.Responses;

namespace CastleInterceptors.Aspects.Exceptions
{
    /// <summary>
    /// TODO : Alper Asenkron hale getir. SSTTEK için gerekli.
    /// </summary>
    public class ExceptionHandlerAspect : MethodInterception
    {
        public override void Intercept(IInvocation invocation)
        {
            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                var instanceType = typeof(Task).IsAssignableFrom(invocation.Method.ReturnType) ? invocation.Method.ReturnType.GenericTypeArguments[0] : invocation.Method.ReturnType;

                var ret = Activator.CreateInstance(instanceType) as IResponseBase;
                ret.IsSuccessful = false;
                ret.Errors = new List<string>
                {
                    "Islem sırasında hata olustu",
                    ex.Message,
                };
                ret.StatusCode = 500;
                
                invocation.ReturnValue = ret;
                var metod = this.GetType().GetMethod("ConvertToGenericType").MakeGenericMethod(invocation.Method.ReturnType.GenericTypeArguments[0]);
                metod.Invoke(null, new object[] { Task.FromResult(invocation.ReturnValue) });
            }
        }


        public async static Task<T> ConvertToGenericType<T>(Task<object> task)
        {
            var result = await task;

            return (T)result;
        }
    }
}
