using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoreIS.Contracts;
using StoreIS.Data;
using StoreIS.Data.Models;
using StoreIS.Models.Product;

namespace StoreIS.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public ProductService(ApplicationDbContext dbContext,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task CreateProduct(CreateProductViewModel model)
        {
            var product = new Product
            {
                Name = model.Name,
                CategoryId = model.CategoryId,
                Price = model.Price,
            };

            if (model.File != null)
            {
                product.Image = await ConvertFileToByteArray(model.File);
            }

            await dbContext.AddAsync(product);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllProducts(string categoryId = null)
        {
            var isParesed = Guid.TryParse(categoryId, out var guidCategoryId);

            IEnumerable<Product> products;

            if (isParesed)
            {
                products = await dbContext
                .Products
                .Where(x => x.CategoryId == guidCategoryId)
                .ToListAsync();
            }
            else
            {
                products = await dbContext
               .Products
               .ToListAsync();
            }


            return mapper.Map<List<ProductViewModel>>(products);
        }

        public async Task<Product> GetById(Guid id)
        {
            return await dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateProduct(EditProductViewModel model)
        {
            var product = await GetById(model.Id);

            if (product == null)
            {
                throw new ArgumentException("Product not found!");
            }

            product.Name = model.Name;
            product.Price = model.Price;
            product.CategoryId = model.CategoryId;

            if (model.File != null)
            {
                product.Image = await ConvertFileToByteArray(model.File);
            }

            await dbContext.SaveChangesAsync();
        }

        private async Task<byte[]> ConvertFileToByteArray(IFormFile file)
        {
            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                var bytes = ms.ToArray();

                return bytes;
            }
        }
    }
}
