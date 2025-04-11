using eCommerce.BusinessLogicLayer.DTO;
using eCommerce.BusinessLogicLayer.ServiceContracts;
using FluentValidation;

namespace eCommerce.ProductsMicroService.API.APIEndpoints;

public static class ProductAPIEndpoints
{
    public static IEndpointRouteBuilder MapProductAPIEndpoints(this IEndpointRouteBuilder app)
    {
        // GET /api/products
        app.MapGet("/api/products", async (IProductsService productsService) =>
        {
            List<ProductResponse?> products = await productsService.GetProducts();
            return Results.Ok(products);
        });

        // GET /api/products/search/product-id/00000000-0000-0000-0000-000000000000
        app.MapGet("/api/products/search/product-id/{ProductID:guid}", async (IProductsService productsService, Guid ProductID) =>
        {
            ProductResponse? product = await productsService.GetProductByCondition(x => x.ProductID == ProductID);
            return Results.Ok(product);
        });

        // GET /api/products/search/xxxxxxxxxxxxxxxxxxx
        app.MapGet("/api/products/search/{filter}", async (IProductsService productsService, string filter) =>
        {
            List<ProductResponse?> productsByName = await productsService.GetProductsByCondition(x => x.ProductName != null && x.ProductName.Contains(filter, StringComparison.OrdinalIgnoreCase));
            List<ProductResponse?> productsByCategory = await productsService.GetProductsByCondition(x => x.Category != null && x.Category.Contains(filter, StringComparison.OrdinalIgnoreCase));
            return Results.Ok(productsByName.Union(productsByCategory).ToList());
        });

        // POST /api/products
        app.MapPost("/api/products", async (IProductsService productsService, IValidator<ProductAddRequest> validator, ProductAddRequest request) =>
        {
            var validation = await validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                Dictionary<string, string[]> errors = validation.Errors.GroupBy(temp => temp.PropertyName).ToDictionary(grp => grp.Key, grp => grp.Select(err => err.ErrorMessage).ToArray());
                return Results.ValidationProblem(errors);
            }

            ProductResponse? product = await productsService.AddProduct(request);

            if (product != null)
                return Results.Created($"/api/products/search/product-id/{product.ProductID}", product);
            else
                return Results.Problem("Error in adding product");
        });

        // PUT /api/products
        app.MapPut("/api/products", async (IProductsService productsService, IValidator<ProductUpdateRequest> validator, ProductUpdateRequest request) =>
        {
            var validation = await validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                Dictionary<string, string[]> errors = validation.Errors.GroupBy(temp => temp.PropertyName).ToDictionary(grp => grp.Key, grp => grp.Select(err => err.ErrorMessage).ToArray());
                return Results.ValidationProblem(errors);
            }

            ProductResponse? product = await productsService.UpdateProduct(request);

            if (product != null)
                return Results.Ok(product);
            else
                return Results.Problem("Error in updateing product");
        });

        // DELETE /api/products/xxxxxxxxxxxxxxxxx
        app.MapDelete("/api/products/{ProductID:guid}", async (IProductsService productsService, Guid ProductID) =>
        {
            bool deleted = await productsService.DeleteProduct(ProductID);
            return deleted ? Results.Ok(true) : Results.Problem("Error in deleting product");
        });

        return app;
    }
}
