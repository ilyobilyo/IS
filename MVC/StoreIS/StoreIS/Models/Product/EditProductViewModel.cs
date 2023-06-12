namespace StoreIS.Models.Product
{
    public class EditProductViewModel : CreateProductViewModel
    {
        public Guid Id { get; set; }
        public string? Image { get; set; }
    }
}
