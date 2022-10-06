using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using CastleInterceptors.Aspects.Exceptions;
using CastleInterceptors.Aspects.Redis;
using CastleInterceptors.Core;

namespace CastleInterceptors.AutoFacModule
{
    public class InterceptorsAutoFacModule : Module
    {
        System.Reflection.Assembly _asm;
        public InterceptorsAutoFacModule(System.Reflection.Assembly asm)
        {
            _asm = asm;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FromRedisCacheAspect>().SingleInstance();
            builder.RegisterType<ExceptionHandlerAspect>().SingleInstance();
            
            var services = _asm.GetExportedTypes().Where(w => w.Name.EndsWith("Manager"));
            foreach (var item in services)
            {
                builder.RegisterType(item).AsImplementedInterfaces()
            .EnableInterfaceInterceptors(new ProxyGenerationOptions
            {
                Selector = new AspectInterceptorSelector()
            });
            }

        }
    }
}

