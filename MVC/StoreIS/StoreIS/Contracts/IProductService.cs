using StoreIS.Data.Models;
using StoreIS.Models.Product;

namespace StoreIS.Contracts
{
    public interface IProductService
    {
        Task CreateProduct(CreateProductViewModel model);
        Task<IEnumerable<ProductViewModel>> GetAllProducts(string categoryId = null);
        Task<Product> GetById(Guid id);
        Task UpdateProduct(EditProductViewModel model);
    }
}
