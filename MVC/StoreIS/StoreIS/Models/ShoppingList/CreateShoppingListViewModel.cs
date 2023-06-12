using System.ComponentModel.DataAnnotations;

namespace StoreIS.Models.ShoppingList
{
    public class CreateShoppingListViewModel
    {
        [Required]
        public string Name { get; set; }

        public string UserId { get; set; }
    }
}
