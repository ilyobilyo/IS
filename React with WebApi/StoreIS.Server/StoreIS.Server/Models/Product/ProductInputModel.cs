using System.ComponentModel.DataAnnotations;

namespace StoreIS.Server.Models.Product
{
    public class ProductInputModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        public IFormFile? File { get; set; }

        [Required]
        public Guid CategoryId { get; set; }
    }
}
