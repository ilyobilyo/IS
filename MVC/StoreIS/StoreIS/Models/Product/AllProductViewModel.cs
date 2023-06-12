using StoreIS.Models.Category;

namespace StoreIS.Models.Product
{
    public class AllProductViewModel
    {
        public IEnumerable<CategoryViewModel> Categories { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }

    }
}
