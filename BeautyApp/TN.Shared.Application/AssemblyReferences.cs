using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TN.Shared.Application.Shared.Command.UpdateExpiredDateItemStatusByCompany;
using TN.Shared.Application.Shared.Command.UpdateItemStatusByCompany;

namespace TN.Shared.Application
{
    public static class AssemblyReferences
    {
        public static IServiceCollection AddSharedApplication(this IServiceCollection services)
        {
            services.AddMediatR(x => x.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            #region FluentValidation
            services.AddScoped<IValidator<UpdateItemStatusByCompanyCommand>, UpdateItemStatusByCompanyCommandValidator>();
            #endregion




            return services;
        }
    }
}
