namespace StoreIS.Server.Models.Product
{
    public class AllProductsResponseModel : ResponseModel
    {
        public IEnumerable<ProductModel> Products { get; set; }
    }
}
