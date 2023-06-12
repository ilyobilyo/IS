using AutoMapper;
using StoreIS.Data.Models;
using StoreIS.Server.Models.Product;

namespace StoreIS.Server.MappingProfile
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductModel>()
                .ForMember(x => x.Photo, y => y.MapFrom(s => Convert.ToBase64String(s.Image)));
        }
    }
}
