﻿using eCommerce.DataAccessLayer.Context;
using eCommerce.DataAccessLayer.Repositories;
using eCommerce.DataAccessLayer.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.DataAccessLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseMySQL(connectionString: configuration.GetConnectionString("DefaultConnection")!);
        });

        services.AddScoped<IProductsRepository, ProductsRepository>();

        return services;
    }
}
