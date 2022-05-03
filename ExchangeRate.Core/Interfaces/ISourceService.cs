using ExchangeRate.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Core.Interfaces
{
    public interface ISourceService
    {
        public Task<IEnumerable<SourseGetDto>> GetRssUrlsAsync();
        public Task<IEnumerable<SourceDropDownDto>> GetSourcesForDropdownSelect();
    }
}
