using AutoMapper;
using StoreIS.Data.Models;
using StoreIS.Models.Category;

namespace StoreIS.MappingProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryViewModel>();
            CreateMap<Category, CreateCategoryViewModel>();
        }
    }
}
