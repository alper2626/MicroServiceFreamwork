using FluentValidation;
using FluentValidation.AspNetCore;

namespace FluentValidationAdapter
{
    public static class FluentValidationStarter
    {
        public static void Configure(this FluentValidationMvcConfiguration fluentValidationMvc)
        {
            var profiles = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(a => a.GetTypes().Where(type => typeof(IValidator).IsAssignableFrom(type)));
            foreach (var item in profiles)
            {
                fluentValidationMvc.RegisterValidatorsFromAssemblyContaining(item);
            }

        }
    }
}
