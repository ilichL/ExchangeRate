using ExchangeRate.Core.Interfaces.Data;
using ExchangeRate.Models.Currency;
using System.ComponentModel.DataAnnotations;

namespace ExchangeRate.Validation
{
    public class CurrencyDetailsModelValidationAttribute : ValidationAttribute
    {
        private readonly IUnitOfWork unitOfWork;
        public override bool IsValid(object? value)
        {
            if (value is CurrencyDetailsModel model)
            {
                var BankName = unitOfWork.Currencies.FindBy(a => a.BankName.Equals(model.BankName));
                var BaseUrl = unitOfWork.Sources.FindBy(a => a.BaseUrl.Equals(model.BaseUrl));
                if (BankName != null && BaseUrl != null)
                    return true;
            }
            return false;
        }
    }
}
