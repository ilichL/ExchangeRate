using ExchangeRate.Core.DTOs;
using ExchangeRate.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Core.Interfaces
{
    public interface ICyrrencyConfigurationService
    {
        public Task<IEnumerable<RssCurrencyDto?>> AggregateCyrrencies();
        public Task<IEnumerable<RssCurrencyDto?>> AggregateAllCyrrenciesFromPrior();
        public Task<IEnumerable<NacBankDto?>> AggregateAllCyrrenciesFromNacBank();
        public Task AggregateAllCyrrenciesAsync();

    }
}
