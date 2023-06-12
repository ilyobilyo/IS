namespace StoreIS.Server.Models.Product
{
    public class ProductModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string? Photo { get; set; }

        public Guid CategoryId { get; set; }
    }
}
