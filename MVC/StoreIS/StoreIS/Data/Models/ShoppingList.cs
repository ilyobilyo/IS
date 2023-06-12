using System.ComponentModel.DataAnnotations;

namespace StoreIS.Data.Models
{
    public class ShoppingList
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<ProductShoppingList> ProductsShoppingLists { get; set; } = new HashSet<ProductShoppingList>();
    }
}
