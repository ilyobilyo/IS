using Microsoft.AspNetCore.Identity;

namespace StoreIS.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<ShoppingList> ShoppingLists { get; set; } = new HashSet<ShoppingList>();
    }
}
