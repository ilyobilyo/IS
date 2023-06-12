using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoreIS.Contracts;
using StoreIS.Data.Models;
using StoreIS.Server.Data;
using StoreIS.Server.Models.Product;

namespace StoreIS.Server.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public ProductService(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ProductResponseModel> CreateProduct(ProductInputModel model)
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

            return new ProductResponseModel
            {
                Message = "Creation successsful",
                Status = StatusCodes.Status201Created,
                Product = mapper.Map<ProductModel>(product)
            };
        }

        public async Task<AllProductsResponseModel> GetAllProducts(string categoryId = null)
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


            return new AllProductsResponseModel
            {
                Status = StatusCodes.Status200OK,
                Products = mapper.Map<IEnumerable<ProductModel>>(products),
            };
        }

        public async Task<ProductResponseModel> GetById(Guid id)
        {
            var product = await dbContext
                .Products
                .FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                throw new ArgumentException("Product is not found");
            }

            return new ProductResponseModel
            {
                Status = StatusCodes.Status200OK,
                Product = mapper.Map<ProductModel>(product),
            };
        }

        public async Task<ProductResponseModel> UpdateProduct(Guid id, ProductInputModel model)
        {
            var product = await dbContext
                .Products
                .FirstOrDefaultAsync(x => x.Id == id);

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

            return new ProductResponseModel
            {
                Status = StatusCodes.Status200OK,
                Product = mapper.Map<ProductModel>(product),
            };
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
