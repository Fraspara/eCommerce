using BusinessLogicLayer.Automapper;
using eCommerce.BusinessLogicLayer.ServiceContracts;
using Microsoft.Extensions.DependencyInjection;
using eCommerce.BusinessLogicLayer.Services;
using FluentValidation;
using eCommerce.BusinessLogicLayer.Validators;



namespace eCommerce.BusinessLogicLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ProductMappingProfile).Assembly);
        services.AddScoped<IProductsService, ProductService>();
        services.AddValidatorsFromAssemblyContaining<ProductAddRequestValidator>();
        return services;
    }
}
