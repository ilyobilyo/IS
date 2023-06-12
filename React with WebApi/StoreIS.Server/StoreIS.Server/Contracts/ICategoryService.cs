using StoreIS.Data.Models;
using StoreIS.Server.Models.Category;

namespace StoreIS.Contracts
{
    public interface ICategoryService
    {
        Task<CategoryResponseModel> GetById(Guid id);
        Task<AllCategoriesResponseModel> GetAll();
        Task<CategoryResponseModel> CreateCategory(CategoryInputModel model);
        Task<CategoryResponseModel> UpdateCategory(Guid id, CategoryInputModel model);
    }
}
