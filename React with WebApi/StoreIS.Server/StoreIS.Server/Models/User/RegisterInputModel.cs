using System.ComponentModel.DataAnnotations;

namespace StoreIS.Server.Models.User
{
    public class RegisterInputModel
    {
        [Required]
        [MaxLength(100)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
