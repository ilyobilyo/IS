using System.ComponentModel.DataAnnotations;

namespace StoreIS.Server.Models.Category
{
    public class CategoryInputModel
    {
        [Required]
        public string Name { get; set; }
    }
}
