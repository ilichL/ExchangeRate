﻿using System.ComponentModel.DataAnnotations;

namespace ExchangeRate.Models.Account
{
    public class AccountRegisterModel
    {
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
