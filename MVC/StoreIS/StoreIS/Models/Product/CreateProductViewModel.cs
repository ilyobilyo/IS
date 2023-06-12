using StoreIS.Models.Category;
using System.ComponentModel.DataAnnotations;

namespace StoreIS.Models.Product
{
    public class CreateProductViewModel
    {
        [Required(ErrorMessage = "Product name is required!")]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        public IFormFile? File { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        public IEnumerable<CategoryViewModel>? Categories { get; set; }
    }
}
