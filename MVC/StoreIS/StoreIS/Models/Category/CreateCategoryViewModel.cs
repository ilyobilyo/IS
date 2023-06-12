using System.ComponentModel.DataAnnotations;

namespace StoreIS.Models.Category
{
    public class CreateCategoryViewModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
