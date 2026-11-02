using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using NV.Feedbacks.Application.Command.AddReview;
using NV.Feedbacks.Application.Feedbacks.Command.UpdateReview;

namespace NV.Feedbacks.Application
{
    public static class AssemblyReferences
    {
        public static IServiceCollection AddFeedbacksApplication(this IServiceCollection services)
        {
            services.AddMediatR(x => x.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            //fluent validation
            services.AddScoped<IValidator<AddReviewCommand>, AddReviewCommandValidator>();
            services.AddScoped<IValidator<UpdateReviewCommand>, UpdateReviewCommandValidator>();
            return services;
        }
    }
}
