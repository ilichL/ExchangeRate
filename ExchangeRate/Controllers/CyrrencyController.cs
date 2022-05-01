using AutoMapper;
using ExchangeRate.Core.DTOs;
using ExchangeRate.Core.Interfaces;
using ExchangeRate.Core.Interfaces.Data;
using ExchangeRate.Models.Currency;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace ExchangeRate.Controllers
{
    [Authorize]
    public class CyrrencyController : Controller
    {
        private readonly ICurrencyService currencyService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;
        private readonly ISourceService sourceService;
        private readonly ICyrrencyConfigurationService service;

        public CyrrencyController(
            IUnitOfWork _unitOfWork,
            ICurrencyService currencyService,
            ICyrrencyConfigurationService service
            )
        {
            this._unitOfWork = _unitOfWork;
            this.currencyService = currencyService;
            this.service = service;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var test = service.AggregateAllCyrrenciesAsync();
            var cyrrency = (await currencyService.GetAllProductsAsync())
                .Select(a => mapper.Map<CurrencyTableViewModel>(a))
                .ToArray();
            return View(cyrrency);//список всех курсов валют
        }//

        public async Task<IActionResult> Test()
        {
            var result = _unitOfWork.Sources.Get().ToArray();
            return View(result);
        }

        public IActionResult Create()
        {

        }


        [HttpPost]
        public async Task<IActionResult> GetCyrrencyFromSource()
        {

            return View();
        }
    }
}
