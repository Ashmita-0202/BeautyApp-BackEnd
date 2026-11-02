using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using NV.Beautician.Application.Beautician.Commands.AddBeautician;
using NV.Beautician.Application.Beautician.Commands.AddBeautiServiceToBeauticians;
using NV.Beautician.Application.Beautician.Commands.AddBeautyServices;
using NV.Beautician.Application.Beautician.Commands.UpdateBeautician;
using NV.Beautician.Application.Beautician.Commands.UpdateBeautyService;

namespace NV.Beautician.Application
{
    public static class AssemblyReferences
    {
        public static IServiceCollection AddBeauticianApplication(this IServiceCollection services)
        {
            services.AddMediatR(x => x.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            //fluent validation
            services.AddScoped<IValidator<AddBeauticianCommand>, AddBeauticianCommandValidator>();
            services.AddScoped<IValidator<UpdateBeauticianCommand>, UpdateBeauticianCommandValidator>();
            services.AddScoped<IValidator<AddBeautyServicesCommand>, AddBeautyServicesCommandValidator>();
              services.AddScoped<IValidator<UpdateBeautyServiceCommand>, UpdateBeautyServiceCommandValidator>();
         services.AddScoped<IValidator<AddServicesToBeauticianCommand>, AddServicesToBeauticianCommandValidator>();
            return services;
        }

    }
}
