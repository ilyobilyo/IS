using System.ComponentModel.DataAnnotations;

namespace StoreIS.Models.ShoppingList
{
    public class ShoppingListViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        [Required]
        public string UserId { get; set; }

        public string CreatedAt { get; set; }
    }
}
