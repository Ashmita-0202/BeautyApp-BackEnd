using Microsoft.Extensions.DependencyInjection;
using NV.Feedbacks.Application.ServiceInterface;
using NV.Feedbacks.Infrastructure.ServiceImpl;

namespace NV.Feedbacks.Infrastructure
{
    public static class AssemblyReferences
    {
        public static IServiceCollection AddFeedbacksInfrastructure(this IServiceCollection services) 
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            #region Inject Dependency
            services.AddScoped<IReviewServices, ReviewServices>();

            #endregion

            return services;
        }
    }
}
