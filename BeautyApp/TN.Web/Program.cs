using NV.Beautician.Application;
using NV.Beautician.Infrastructure;
using NV.Booking.Application;
using NV.Booking.Infrastructure;
using NV.Feedbacks.Application;
using NV.Feedbacks.Infrastructure;
using NV.Inventory.Application;
using NV.Inventory.Infrastructure;
using NV.Payment.Application;
using NV.Payment.Infrastructure;
using NV.Shared.Infrastructure;
using TN.Authentication.Application;
using TN.Authentication.Infrastructure;
using TN.Setup.Application;
using TN.Setup.Infrastructure;
using TN.Shared.Application;
using TN.Shared.Infrastructure;
using TN.Shared.Infrastructure.CustomMiddleware.GlobalErrorHandling;

using TN.Web.Configs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


#region CORS Enable
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });

});

#endregion

ConfigurationManager configuration = builder.Configuration;


//"message" and "Message" will both map to Message.
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNameCaseInsensitive = true;
    options.SerializerOptions.IncludeFields = true;
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // Use PascalCase
    });






builder.Services
    .AddSharedApplication()
    .AddSharedInfrastructure(configuration);
builder.Services
    .AddAuthenticationApplication()
    .AddAuthenticationInfrastructure();

builder.Services
    .AddSetupApplication()
    .AddSetupInfrastructure();

builder.Services
    .AddBeauticianApplication()
    .AddBeauticianInfrastructure();

builder.Services
    .AddBookingApplication()
    .AddBookingInfrastructure();

builder.Services
    .AddFeedbacksApplication()
    .AddFeedbacksInfrastructure();
builder.Services
    .AddInventoryApplication()
    .AddInventoryInfrastructure();

builder.Services.AddPaymentApplication()
    .AddPaymentInfrastructure();

ApplicationBuilderConfig.Inject(builder);

var app = builder.Build();

// Apply migrations during application startup
//using (var scope = app.Services.CreateScope())
//{
//    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//    context.Database.Migrate();  // Apply any pending migrations
//}



app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();
app.UseStaticFiles();

ApplicationConfiguration.Inject(app);

app.Run();
