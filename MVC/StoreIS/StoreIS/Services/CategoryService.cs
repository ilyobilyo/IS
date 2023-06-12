using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoreIS.Contracts;
using StoreIS.Data;
using StoreIS.Data.Models;
using StoreIS.Models.Category;

namespace StoreIS.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public CategoryService(ApplicationDbContext dbContext,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<bool> CreateCategory(CreateCategoryViewModel model)
        {
            var category = new Category()
            {
                Name = model.Name,
            };

            await dbContext.AddAsync(category);
            await dbContext.SaveChangesAsync();

            return true;

        }

        public async Task<IEnumerable<CategoryViewModel>> GetAll()
        {
            var categories = await dbContext
                .Categories
                .ToListAsync();

            return mapper.Map<List<CategoryViewModel>>(categories);
        }

        public async Task<Category> GetById(Guid id)
        {
            return await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> UpdateCategory(Guid id, CreateCategoryViewModel model)
        {
            var categoryToUpdate = await GetById(id);

            if (categoryToUpdate == null)
            {
                return false;
            }

            categoryToUpdate.Name = model.Name;

            await dbContext.SaveChangesAsync();

            return true;

        }
    }
}
