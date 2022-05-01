using System.ComponentModel.DataAnnotations;

namespace ExchangeRate.Models.Currency
{
    public class CurrencyDetailsModel
    {
        //для View Create
        [Required]
        public Guid Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
        public decimal SalePrice { get; set; }
        public decimal BuyPrice { get; set; }
        public string BankName { get; set; }
        public DateTime CreationDate { get; set; }
        public string SiteName;
    }
}
