using Microsoft.Extensions.DependencyInjection;
using NV.Booking.Application.ServiceInterface;
using NV.Booking.Infrastructure.ServiceImpl;

namespace NV.Booking.Infrastructure
{
    public static class AssemblyReferences
    {
        public static IServiceCollection AddBookingInfrastructure(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            #region Inject Dependency
            services.AddScoped<IBookingsServices, BookingsServices>();

            #endregion
            return services;
        }
    }
}
