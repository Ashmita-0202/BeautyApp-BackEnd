using Microsoft.Extensions.DependencyInjection;
using NV.Inventory.Application.ServiceInterface;
using NV.Inventory.Infrastructure.ServiceImpl;


namespace NV.Inventory.Infrastructure
{
    public static class AssemblyReferencesBase
    {
        public static IServiceCollection AddInventoryInfrastructure(this IServiceCollection services)
        {

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            #region Inject Dependency
            services.AddScoped<IProductServices, ProductServices>();

            #endregion

            return services;
        }
    }
}