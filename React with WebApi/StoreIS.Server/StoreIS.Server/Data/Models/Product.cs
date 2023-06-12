using System.ComponentModel.DataAnnotations;

namespace StoreIS.Data.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        public byte[]? Image { get; set; }

        [Required]
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
