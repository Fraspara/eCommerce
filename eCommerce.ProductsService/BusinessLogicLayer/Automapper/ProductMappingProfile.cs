using AutoMapper;
using eCommerce.BusinessLogicLayer.DTO;
using eCommerce.DataAccessLayer.Entities;

namespace BusinessLogicLayer.Automapper;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<ProductUpdateRequest, Product>();
        CreateMap<Product, ProductResponse>();
        CreateMap<ProductAddRequest, Product>().ForMember(dest => dest.ProductID, opt => opt.Ignore());
    }
}
