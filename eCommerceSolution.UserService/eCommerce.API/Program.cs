using eCommerce.API.Middlewares;
using eCommerce.Core;
using eCommerce.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add infrastructure services
builder.Services.AddInfrastructure();

// Add core services
builder.Services.AddCore();

// Add controllers to the service collection
builder.Services.AddControllers();

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
