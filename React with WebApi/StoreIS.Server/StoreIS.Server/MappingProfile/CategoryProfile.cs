using AutoMapper;
using StoreIS.Data.Models;
using StoreIS.Server.Models.Category;

namespace StoreIS.Server.MappingProfile
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryModel>();
        }
    }
}
