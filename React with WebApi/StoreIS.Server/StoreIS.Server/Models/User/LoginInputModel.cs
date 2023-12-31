﻿using System.ComponentModel.DataAnnotations;

namespace StoreIS.Server.Models.User
{
    public class LoginInputModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
