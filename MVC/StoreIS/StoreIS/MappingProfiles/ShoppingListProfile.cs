using AutoMapper;
using StoreIS.Data.Models;
using StoreIS.Models.ShoppingList;

namespace StoreIS.MappingProfiles
{
    public class ShoppingListProfile : Profile
    {
        public ShoppingListProfile()
        {
            CreateMap<ShoppingList, ShoppingListViewModel>()
                .ForMember(x => x.CreatedAt, y => y.MapFrom(s => s.CreatedAt.ToString("dd-MM-yyyy")));

            CreateMap<ShoppingList, ShopingListDetailsViewModel>()
                .ForMember(x => x.CreatedAt, y => y.MapFrom(s => s.CreatedAt.ToString("dd-MM-yyyy")))
                .ForMember(x => x.Products, y => y.MapFrom(x => x.ProductsShoppingLists));

            CreateMap<ShoppingList, CreateShoppingListViewModel>();
            CreateMap<ShoppingList, EditListViewModel>();
        }
    }
}
