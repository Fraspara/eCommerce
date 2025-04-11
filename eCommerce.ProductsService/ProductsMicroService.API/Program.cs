using eCommerce.BusinessLogicLayer;
using eCommerce.DataAccessLayer;
using eCommerce.ProductsMicroService.API.APIEndpoints;
using FluentValidation.AspNetCore;
using eCommerce.ProductsMicroService.API.Middleware;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add DAL e BLL services
builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddBusinessLogicLayer();

builder.Services.AddControllers();

// FluentValidations
builder.Services.AddFluentValidationAutoValidation();

// Add model binder to read values from JSON to enum
builder.Services.ConfigureHttpJsonOptions(opt => opt.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("Http://localhost:4200")
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

var app = builder.Build();

// Exception middleware
app.UseExceptionHandlingMiddleware();
app.UseRouting();

// Cors
app.UseCors(builder =>
{
    builder.AllowAnyOrigin();
    builder.AllowAnyMethod();
    builder.AllowAnyHeader();
});

// Swagger
app.UseSwagger();
app.UseSwaggerUI();

// Auth
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapProductAPIEndpoints();

app.Run();
