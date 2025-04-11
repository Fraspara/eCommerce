using eCommerce.API.Middlewares;
using eCommerce.Core;
using eCommerce.Core.Automapper;
using eCommerce.Infrastructure;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
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
// FluentValidations
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:4200")
        .AllowAnyOrigin()
        .AllowAnyMethod();
    });
});

// Build the web app
var app = builder.Build();

// Middlewares
app.UseExceptionHandlingMiddleware();

// Routing
app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI();

// Cors
app.UseCors(builder =>
{
    builder.AllowAnyOrigin();
    builder.AllowAnyMethod();
    builder.AllowAnyHeader();
});

// Auth
app.UseAuthentication();
app.UseAuthorization();

//Controller routes
app.MapControllers();

app.Run();
