using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using NV.Booking.Application.Booking.Command.AddBooking;
using NV.Booking.Application.Booking.Command.UpdateBooking;
using NV.Booking.Application.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NV.Booking.Application
{
    public static class AssemblyReferences
    {
        public static IServiceCollection AddBookingApplication(this IServiceCollection services)
        {
            services.AddMediatR(x => x.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            //fluent validation
            services.AddScoped<IValidator<AddBookingCommand>, AddBookingCommandValidator>();
            services.AddScoped<IValidator<UpdateBookingCommand>, UpdateBookingCommandValidator>();
           

            return services;
        }
    }
}
