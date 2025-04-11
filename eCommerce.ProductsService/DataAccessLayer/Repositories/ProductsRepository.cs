using eCommerce.DataAccessLayer.Context;
using eCommerce.DataAccessLayer.Entities;
using eCommerce.DataAccessLayer.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace eCommerce.DataAccessLayer.Repositories;

public class ProductsRepository : IProductsRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ProductsRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Product?> AddProduct(Product product)
    {
        _dbContext.Products.Add(product);
        await _dbContext.SaveChangesAsync();
        return product;
    }

    public async Task<bool> DeleteProduct(Guid productID)
    {
        var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.ProductID == productID);
        if (product == null)
            return false;

        _dbContext.Remove(product);
        int rowCount = await _dbContext.SaveChangesAsync();
        return rowCount>0;
    }

    public async Task<Product?> GetProductByCondition(Expression<Func<Product, bool>> condition)
    {
        return await _dbContext.Products.FirstOrDefaultAsync(condition);
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await _dbContext.Products.ToListAsync();
    }

    public async Task<IEnumerable<Product?>> GetProductsByCondition(Expression<Func<Product, bool>> condition)
    {
        return await _dbContext.Products.Where(condition).ToListAsync();
    }

    public async Task<Product?> UpdateProduct(Product product)
    {
        var item = await _dbContext.Products.FirstOrDefaultAsync(x => x.ProductID == product.ProductID);
        if (item == null)
            return null;

        item.ProductName = product.ProductName;
        item.UnitPrice = product.UnitPrice;
        item.QuantityInStock = product.QuantityInStock;
        item.Category = product.Category;

        await _dbContext.SaveChangesAsync();
        return item;
    }
}
