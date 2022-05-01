using System.ComponentModel.DataAnnotations;
namespace ExchangeRate.Models.Account
{
    public class AccountLoginModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
