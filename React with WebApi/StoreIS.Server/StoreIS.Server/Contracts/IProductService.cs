using StoreIS.Server.Models.Product;

namespace StoreIS.Contracts
{
    public interface IProductService
    {
        Task<ProductResponseModel> CreateProduct(ProductInputModel model);
        Task<AllProductsResponseModel> GetAllProducts(string categoryId = null);
        Task<ProductResponseModel> GetById(Guid id);
        Task<ProductResponseModel> UpdateProduct(Guid id, ProductInputModel model);
    }
}
