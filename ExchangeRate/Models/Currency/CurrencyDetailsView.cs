using ExchangeRate.Models.Comment;

namespace ExchangeRate.Models.Currency
{
    public class CurrencyDetailsView
    {//для View/Product/Details.cshtml
        public CurrencyDetailsModel productDetailsModel { get; set; }
        public List<CommentModel> Comments { get; set; }

    }
}
