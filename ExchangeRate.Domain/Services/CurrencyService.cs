using AutoMapper;
using ExchangeRate.Core.DTOs;
using ExchangeRate.Core.Interfaces;
using ExchangeRate.Core.Interfaces.Data;
using Microsoft.EntityFrameworkCore;

namespace ExchangeRate.Domain.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CurrencyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<CurrencyDTO>> GetAllProductsAsync()
        {
            return await unitOfWork.Currencies.Get().
                Select(product => mapper.Map<CurrencyDTO>(product)).
                ToArrayAsync();
        }
        public async Task<CurrencyDTO> GetAllProductWithAllNavigationProperties(Guid id)
        {
            var product = await unitOfWork.Currencies.GetByIdWithIncludes(id,
                product => product.Site,
                product => product.Comments);
            return mapper.Map<CurrencyDTO>(product);
        }

        public async Task<IEnumerable<CurrencyDTO>> GetProductsWithSiteNameBySiteId(Guid sourceId)
        {
            var products = await unitOfWork.Currencies.FindBy(product => product.ID.Equals(sourceId),
                product => product.Site);
            var result = await products
                .Select(article => mapper.Map<CurrencyDTO>(article))
                .ToArrayAsync();
            return result;
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            await unitOfWork.Currencies.Delete(id);
            return await unitOfWork.Save();
        }
    }
}
