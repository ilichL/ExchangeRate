using AutoMapper;
using ExchangeRate.Core.DTOs;
using ExchangeRate.Core.Interfaces;
using ExchangeRate.Core.Interfaces.Data;
using ExchangeRate.Data.Entities;
using ExchangeRate.Models;
using ExchangeRate.Models.Currency;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Linq;

namespace ExchangeRate.Controllers
{
    //[Authorize]
    public class CyrrencyController : Controller
    {
        private readonly ICurrencyService currencyService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;
        private readonly ISourceService sourceService;
        private readonly ICyrrencyConfigurationService service;
        private readonly ILogger<CyrrencyController> _logger;

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

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            var deleteModel = new DeleteModel() { Id = id };
            return View(deleteModel);
        }

        [HttpGet]
        public IActionResult Delete(DeleteModel model)
        {
            currencyService.DeleteAsync(model.Id);
            return RedirectToAction("");//куда вернемсяпосле удаления
        }

        [HttpGet]
        public IActionResult Create()
        {
            CurrencyDetailsModel model = new CurrencyDetailsModel()
            {
                //Id = id
            };
            return View(model);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CurrencyDetailsModel model)
        {
            try
            {
                if (!string.IsNullOrEmpty(model.BaseUrl))
                {
                    var source = await _unitOfWork.Sources.FindBy(a => a.BaseUrl.Equals(model.BaseUrl));

                    if (source != null)
                    {
                        var id = source.FirstOrDefault(a => a.Equals(model.BaseUrl));
                        Currency cyr = new Currency()
                        {
                            EurBuy = model.EurBuy,
                            EurSell = model.EurSell,
                            RubBuy = model.RubBuy,
                            RubSell = model.RubSell,
                            UsdBuy = model.UsdBuy,
                            UsdSell = model.UsdSell,
                            BankName = model.BankName,
                            CreationDate = DateTime.Now,
                            SiteID = id.ID
                        };
                        await _unitOfWork.Currencies.Add(cyr);
                        await _unitOfWork.Save();
                        return RedirectToAction("");//вернемся к списку валют
                    }
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var cyr = await _unitOfWork.Currencies.GetById(id);
            var source = await _unitOfWork.Sources.GetById(cyr.SiteID);

            CurrencyDetailsModel model = new CurrencyDetailsModel()
            {
                EurBuy = cyr.EurBuy,
                EurSell = cyr.EurSell,
                RubBuy = cyr.RubBuy,
                RubSell = cyr.RubSell,
                UsdBuy = cyr.UsdBuy,
                UsdSell = cyr.UsdSell,
                BankName = cyr.BankName,
                CreationDate = DateTime.Now,
                BaseUrl = source.BaseUrl
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CurrencyDetailsModel model)
        {
            if (!string.IsNullOrEmpty(model.BaseUrl))
            {
                var source = await _unitOfWork.Sources.FindBy(a => a.BaseUrl.Equals(model.BaseUrl));

                if (source != null)
                {
                    var id = source.FirstOrDefault(a => a.Equals(model.BaseUrl));
                    Currency cyr = new Currency()
                    {
                        EurBuy = model.EurBuy,
                        EurSell = model.EurSell,
                        RubBuy = model.RubBuy,
                        RubSell = model.RubSell,
                        UsdBuy = model.UsdBuy,
                        UsdSell = model.UsdSell,
                        BankName = model.BankName,
                        CreationDate = DateTime.Now,
                        SiteID = id.ID
                    };
                    await _unitOfWork.Currencies.Add(cyr);
                    await _unitOfWork.Save();
                    return RedirectToAction("");//вернемся к списку валют
                }
            }

            return BadRequest();

        }
    }
}
