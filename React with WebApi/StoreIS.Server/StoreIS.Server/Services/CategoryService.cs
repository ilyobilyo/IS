using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoreIS.Contracts;
using StoreIS.Data.Models;
using StoreIS.Server.Data;
using StoreIS.Server.Models.Category;

namespace StoreIS.Server.Services
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

        public async Task<CategoryResponseModel> CreateCategory(CategoryInputModel model)
        {
            var category = new Category()
            {
                Name = model.Name,
            };

            await dbContext.AddAsync(category);
            await dbContext.SaveChangesAsync();

            return new CategoryResponseModel 
            { 
                Message = "Creation successfull" ,
                Status = StatusCodes.Status201Created,
                Category = mapper.Map<CategoryModel>(category),
            };
        }

        public async Task<AllCategoriesResponseModel> GetAll()
        {
            var categories = await dbContext
                .Categories
                .ToListAsync();

            return new AllCategoriesResponseModel
            {
                Status = StatusCodes.Status200OK,
                Categories = mapper.Map<List<CategoryModel>>(categories),
            };
        }

        public async Task<CategoryResponseModel> GetById(Guid id)
        {
            var category = await dbContext
                .Categories
                .FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
            {
                throw new ArgumentException("Category is not found");
            }

            return new CategoryResponseModel
            {
                Status = StatusCodes.Status200OK,
                Category = mapper.Map<CategoryModel>(category),
            };
        }

        public async Task<CategoryResponseModel> UpdateCategory(Guid id, CategoryInputModel model)
        {
            var categoryToUpdate = await dbContext
                .Categories
                .FirstOrDefaultAsync(x => x.Id == id);

            if (categoryToUpdate == null)
            {
                throw new ArgumentException("Category not found!");
            }

            categoryToUpdate.Name = model.Name;

            await dbContext.SaveChangesAsync();

            return new CategoryResponseModel
            {
                Status = StatusCodes.Status200OK,
                Category = mapper.Map<CategoryModel>(categoryToUpdate),
            };
        }
    }
}
