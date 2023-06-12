using StoreIS.Models.Product;

namespace StoreIS.Models.ShoppingList
{
    public class ShopingListDetailsViewModel : ShoppingListViewModel
    {
        public IEnumerable<ShoppingListProductViewModel> Products { get; set; }
    }
}
