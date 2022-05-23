using AutoMapper;
using ExchangeRate.Core.DTOs;
using ExchangeRate.Core.Interfaces;
using ExchangeRate.Core.Interfaces.Data;
using ExchangeRate.Data.Entities;
using Microsoft.Extensions.Logging;

namespace ExchangeRate.Domain.Services
{
    public class CyrrencyConfigurationService : ICyrrencyConfigurationService
    {
        private readonly IRssService rssService;
        private readonly ISourceService sourceService;
        private readonly ILogger<CyrrencyConfigurationService> _logger;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        
        public CyrrencyConfigurationService(
            IRssService rssService,
            ISourceService sourceService,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<CyrrencyConfigurationService> _logger
            )
        {
            this.rssService = rssService;
            this.sourceService = sourceService;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this._logger = _logger;
        }
        public async Task<IEnumerable<RssCurrencyDto?>> AggregateCyrrencies()
        {
            try
            {
               var rssdto = await sourceService.GetRssUrlsAsync();//SourseGetDto все ссылки
               
                var list = rssdto.Take(2)
                    .Select(r => rssService.GetCyrrency(r));
                return list;
                ;//ррб абсолютбанк

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }

        }
        public async Task<IEnumerable<RssCurrencyDto?>> AggregateAllCyrrenciesFromPrior()
        {
            try
            {
                var rssdto = await sourceService.GetRssUrlsAsync();
                var priorlist = rssdto.Skip(2).Take(1)
                    .Select(dto => rssService.GetCyrrencyPrior(dto));//RssCurrencyDto
                return priorlist;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }

        }

        public async Task<IEnumerable<NacBankDto?>> AggregateAllCyrrenciesFromNacBank()
        {
            try
            {
                var rssdto = await sourceService.GetRssUrlsAsync();
                var NacBanklist = rssdto.Skip(3).Take(1)
                    .Select(dto => rssService
                    .GetNacBankAsync(dto));//NacBankDto
                return (IEnumerable<NacBankDto?>)NacBanklist;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }

        }


        public async Task AggregateAllCyrrenciesAsync()
        {
            var list = await AggregateCyrrencies();
            var result = list.Select(dto => mapper.Map<Currency>(dto));
            await unitOfWork.Currencies.AddRange(result);
            await unitOfWork.Save();

            var list2 = await AggregateAllCyrrenciesFromPrior();
            var result2 = list2.Select(dto => mapper.Map<Currency>(dto));
            await unitOfWork.Currencies.AddRange(result2);

            //var list3 = AggregateAllCyrrenciesFromNacBank();
            //list.Result.Select(dto => mapper.Map<Source>(dto));
            //await unitOfWork.Currencies.AddRange((IEnumerable<Currency>)list);

            await unitOfWork.Save();
        }

    }
}


