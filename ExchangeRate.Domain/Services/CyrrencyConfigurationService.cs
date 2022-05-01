using AutoMapper;
using ExchangeRate.Core.DTOs;
using ExchangeRate.Core.Interfaces;
using ExchangeRate.Core.Interfaces.Data;
using ExchangeRate.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task <IEnumerable<RssCurrencyDto?>> AggregateCyrrencies()
        {
            try
            {
               var rssdto = await sourceService.GetRssUrlsAsync();//SourseGetDto


                var list = rssdto.Take(2)
                .Select(dto => rssService
                .GetCyrrencyAsync(dto)).ToList();//RssCurrencyDto
                ;//ррб абсолютбанк

                return (IEnumerable<RssCurrencyDto>)list;
                //Unable to cast object of type
                //'System.Collections.Generic.List`1[System.Threading.Tasks.Task`1[ExchangeRate.Core.DTOs.RssCurrencyDto]]'
                //to type 'System.Collections.Generic.IEnumerable`1[ExchangeRate.Core.DTOs.RssCurrencyDto]'.

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
                var priorlist = rssdto.Skip(3).Take(1)
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
                var NacBanklist = rssdto.Skip(2).Take(1)
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
            var list = AggregateCyrrencies();
            var result = list.Result.Select(dto => mapper.Map<Currency>(dto));
            int i = 0;
            await unitOfWork.Currencies.AddRange(result);

            var list2 = AggregateAllCyrrenciesFromPrior();
            list.Result.Select(dto => mapper.Map<Source>(dto));
            await unitOfWork.Currencies.AddRange((IEnumerable<Currency>)list);

            var list3 = AggregateAllCyrrenciesFromNacBank();
            list.Result.Select(dto => mapper.Map<Source>(dto));
            await unitOfWork.Currencies.AddRange((IEnumerable<Currency>)list);

            await unitOfWork.Save();
        }

    }
}


