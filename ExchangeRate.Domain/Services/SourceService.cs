

using AutoMapper;
using ExchangeRate.Core.DTOs;
using ExchangeRate.Core.Interfaces;
using ExchangeRate.Core.Interfaces.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ExchangeRate.Domain.Services
{
    public class SourceService : ISourceService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<SourceService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRssService sourceservice;
        public SourceService(IMapper mapper, IUnitOfWork unitOfWork,
            ILogger<SourceService> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }


        public async Task<IEnumerable<SourseGetDto>> GetRssUrlsAsync()
        {
            try
            {
                var result = await _unitOfWork.Sources.Get()
                    .OrderBy(source => source.ID)//сортируем по возрастанию id
                    .Select(a => _mapper.Map<SourseGetDto>(a))
                    .ToListAsync();
                return result;//возвращаем лист SourseGetDto
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return null;
            }
        }

        public async Task<Guid> GetSourceByUrl(string url)
        {
            var domain = string.Join(".",
                new Uri(url).Host
                    .Split('.')
                    .TakeLast(2)
                    .ToList());
            return (await _unitOfWork.Sources.Get()
                       .FirstOrDefaultAsync(source => source.BaseUrl.Equals(domain)))?.ID
                   ?? Guid.Empty;

        }

        public async Task<IEnumerable<SourceDropDownDto>> GetSourcesForDropdownSelect()
        {
            return await _unitOfWork.Sources.Get().Select(source => new SourceDropDownDto()
            {
                Id = source.ID,
               // Name = source.Name
            }).ToListAsync();
        }

    }
}
