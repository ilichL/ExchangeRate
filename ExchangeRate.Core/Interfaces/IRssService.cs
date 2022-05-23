using ExchangeRate.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Core.Interfaces
{
    public interface IRssService
    {
        public RssCurrencyDto GetCyrrency(SourseGetDto dto);
        public Task<NacBankDto> GetNacBankAsync(SourseGetDto dto);
        public RssCurrencyDto GetCyrrencyPrior(SourseGetDto dto);

    }
}
