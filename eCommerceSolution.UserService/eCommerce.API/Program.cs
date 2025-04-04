using eCommerce.API.Middlewares;
using eCommerce.Core;
using eCommerce.Core.Automapper;
using eCommerce.Core.Entities;
using eCommerce.Infrastructure;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add infrastructure services extension
builder.Services.AddInfrastructure();

// Add core services extension
builder.Services.AddCore();

// Add controllers to the service collection
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

// Add automapper
builder.Services.AddAutoMapper(typeof(ApplicationUserMappingProfile).Assembly);

// Build the web app
var app = builder.Build();

// Middlewares
app.UseExceptionHandlingMiddleware();

// Routing
app.UseRouting();

// Auth
app.UseAuthentication();
app.UseAuthorization();

//Controller routes
app.MapControllers();

app.Run();
