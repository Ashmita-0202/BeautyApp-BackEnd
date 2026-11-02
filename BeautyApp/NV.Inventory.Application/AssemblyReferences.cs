using System.Reflection;
using System.Runtime.CompilerServices;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using NV.Inventory.Application.Inventory.Command.AddProduct;
using NV.Inventory.Application.Inventory.Command.UpdateProduct;

namespace NV.Inventory.Application
{
    public static class AssemblyReferences
    {
        public static IServiceCollection AddInventoryApplication(this IServiceCollection services) 
        {
            services.AddMediatR(x => x.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            //fluent validation
            services.AddScoped<IValidator<AddProductCommand>, AddProductCommandValidator>();
            services.AddScoped<IValidator<UpdateProductCommand>, UpdateProductCommandValidator>();


            return services;
        
        }
    }
}

