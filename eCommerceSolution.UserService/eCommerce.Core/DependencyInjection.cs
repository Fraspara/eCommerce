using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.Core;

public static class DependencyInjection
{
    /// <summary>
    /// Extension method to add infrastructure service to the dependency injection container
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        // Add services to the IoC container.

        return services;
    }
}
