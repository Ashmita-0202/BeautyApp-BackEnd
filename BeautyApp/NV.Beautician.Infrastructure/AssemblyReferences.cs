using Microsoft.Extensions.DependencyInjection;
using NV.Beautician.Application.ServiceInterface;
using NV.Beautician.Infrastructure.ServiceImpl;

namespace NV.Beautician.Infrastructure
{
    public static class AssemblyReferences
    {
        public static IServiceCollection AddBeauticianInfrastructure(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



            #region Inject Dependency
            services.AddScoped<IBeauticianService, BeauticianService>();
            services.AddScoped<IBeautyServices, BeautyService>();
            services.AddScoped<IServicesBeautician, ServicesBeauticians>();
          
            #endregion
            return services;
        }
    }
}
