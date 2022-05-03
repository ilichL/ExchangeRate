using ExchangeRate.Validation;
using System.ComponentModel.DataAnnotations;

namespace ExchangeRate.Models.Currency
{
            
    // [CurrencyDetailsModelValidationAttribute]
    public class CurrencyDetailsModel
    {
        //для View Create

        [Required]
        public decimal EurBuy { get; set; }
        [Required]
        public decimal EurSell { get; set; }
        [Required]
        public decimal RubBuy { get; set; }
        [Required]
        public decimal RubSell { get; set; }
        [Required]
        public decimal UsdBuy { get; set; }
        [Required]
        public decimal UsdSell { get; set; }
        [Required]
        public string BankName { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        public string BaseUrl;
    }
}
