using ExchangeRate.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Core.Interfaces
{
    public interface ICurrencyService
    {
        public Task<IEnumerable<CurrencyDTO>> GetAllProductsAsync();
        public Task<CurrencyDTO> GetAllProductWithAllNavigationProperties(Guid id);
        Task<int> DeleteAsync(Guid id);
    }
}
