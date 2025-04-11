using AutoMapper;
using eCommerce.BusinessLogicLayer.DTO;
using eCommerce.BusinessLogicLayer.ServiceContracts;
using eCommerce.DataAccessLayer.Entities;
using eCommerce.DataAccessLayer.RepositoryContracts;
using FluentValidation;
using FluentValidation.Results;
using System.Linq.Expressions;

namespace eCommerce.BusinessLogicLayer.Services;

public class ProductService(
    IValidator<ProductAddRequest> _productAddRequestValidator,
    IValidator<ProductUpdateRequest> _productUpdateRequestValidator,
    IMapper _mapper,
    IProductsRepository _productRepository) : IProductsService
{
    public async Task<ProductResponse?> AddProduct(ProductAddRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        // Validation
        ValidationResult validationResult = await _productAddRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
            throw new ArgumentException(string.Join(", ", validationResult.Errors.Select(temp => temp.ErrorMessage)));

        Product product = _mapper.Map<Product>(request);

        Product? item = await _productRepository.AddProduct(product);
        if (item == null)
            return null;

        return _mapper.Map<ProductResponse>(item);
    }

    public async Task<bool> DeleteProduct(Guid productID)
    {
        Product? item = await _productRepository.GetProductByCondition(x => x.ProductID == productID);

        if (item == null)
            return false;

        return await _productRepository.DeleteProduct(productID);
    }

    public async Task<ProductResponse?> GetProductByCondition(Expression<Func<Product, bool>> conditionExpression)
    {
        Product? product = await _productRepository.GetProductByCondition(conditionExpression);

        if (product == null)
            return null;

        return _mapper.Map<ProductResponse?>(product);
    }

    public async Task<List<ProductResponse?>> GetProducts()
    {
        IEnumerable<Product?> products = await _productRepository.GetProducts();
        return _mapper.Map<List<ProductResponse?>>(products);
    }

    public async Task<List<ProductResponse?>> GetProductsByCondition(Expression<Func<Product, bool>> conditionExpression)
    {
        IEnumerable<Product?> products = await _productRepository.GetProductsByCondition(conditionExpression);
        return _mapper.Map<List<ProductResponse?>>(products);
    }

    public async Task<ProductResponse?> UpdateProduct(ProductUpdateRequest request)
    {
        Product? item = await _productRepository.GetProductByCondition(x => x.ProductID == request.ProductID);

        if (item == null)
            return null;

        var validation = await _productUpdateRequestValidator.ValidateAsync(request);
        if (!validation.IsValid)
        {
            throw new ArgumentNullException(string.Join(", ", validation.Errors.Select(x => x.ErrorMessage)));
        }

        Product product = _mapper.Map<Product>(item);
        var updatedProduct = await _productRepository.UpdateProduct(product);

        return _mapper.Map<ProductResponse?>(product);
    }
}
