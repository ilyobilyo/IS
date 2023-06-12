using StoreIS.Data.Models;
using StoreIS.Models.Category;

namespace StoreIS.Contracts
{
    public interface ICategoryService
    {
        Task<Category> GetById(Guid id);
        Task<IEnumerable<CategoryViewModel>> GetAll();
        Task<bool> CreateCategory(CreateCategoryViewModel model);
        Task<bool> UpdateCategory(Guid id, CreateCategoryViewModel model);
    }
}
