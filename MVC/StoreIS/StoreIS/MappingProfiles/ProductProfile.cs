using AutoMapper;
using StoreIS.Data.Models;
using StoreIS.Models.Product;

namespace StoreIS.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductViewModel>()
                .ForMember(x => x.Image, y => y.MapFrom(s => Convert.ToBase64String(s.Image)));
            CreateMap<Product, EditProductViewModel>()
                .ForMember(x => x.Image, y => y.MapFrom(s => Convert.ToBase64String(s.Image)));
            CreateMap<ProductShoppingList, ShoppingListProductViewModel>()
                .ForMember(x => x.Id, y => y.MapFrom(s => s.ProductId))
                .ForMember(x => x.Name, y => y.MapFrom(s => s.Product.Name))
                .ForMember(x => x.Price, y => y.MapFrom(x => x.Product.Price))
                .ForMember(x => x.Image, y => y.MapFrom(s => Convert.ToBase64String(s.Product.Image)));
        }
    }
}
