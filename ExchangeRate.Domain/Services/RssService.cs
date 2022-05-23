using AutoMapper;
using Microsoft.Extensions.Logging;
using System.ServiceModel.Syndication;
using System.Xml;
using HtmlAgilityPack;
using ExchangeRate.Core.DTOs;
using ExchangeRate.Core.Interfaces;
using System.Globalization;
using ExchangeRate.Data;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace ExchangeRate.Domain.Services
{
    public class RssService : IRssService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<SourceService> _logger;
        private readonly Context _db;
        public RssService(IMapper mapper,
            ILogger<SourceService> logger,
            Context db)
        {
            _mapper = mapper;
            _logger = logger;
            _db = db;
        }

        public RssCurrencyDto GetCyrrency(SourseGetDto dto)
        {//для 1 банка
            try
            {
                using (HttpClientHandler hdl = new HttpClientHandler { })
                {
                    using (var clnt = new HttpClient(hdl))
                    {
                        using (HttpResponseMessage resp = clnt.GetAsync(dto.BaseUrl).Result)
                        {
                            var html = resp.Content.ReadAsStringAsync().Result;
                            HtmlDocument doc = new HtmlDocument();
                            doc.LoadHtml(html);
                            IFormatProvider format = new NumberFormatInfo { NumberDecimalSeparator = "." };
                            RssCurrencyDto rssCurrencyDto = new RssCurrencyDto()
                            {
                                ID = Guid.NewGuid(),
                                SiteID = dto.ID,
                                EurBuy = decimal.Parse
                                    (doc.DocumentNode.SelectNodes(dto.EurBuyNode).Single().InnerText, format),
                                EurSell = decimal.Parse
                                    (doc.DocumentNode.SelectNodes(dto.EurSellNode).Single().InnerText, format),
                                RubBuy = decimal.Parse
                                    (doc.DocumentNode.SelectNodes(dto.RubBuyNode).Single().InnerText, format),
                                RubSell = decimal.Parse
                                    (doc.DocumentNode.SelectNodes(dto.RubSellNode).Single().InnerText, format),
                                UsdBuy = decimal.Parse
                                    (doc.DocumentNode.SelectNodes(dto.UsdBuyNode).Single().InnerText, format),
                                UsdSell = decimal.Parse
                                    (doc.DocumentNode.SelectNodes(dto.UsdSellNode).Single().InnerText, format),
                                BankName = dto.BankName,
                                CreationDate = DateTime.Now
                            };
                            return rssCurrencyDto;//тут лежат значения покупки и продажи
                        }//вывод, сохранение в бд
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return null;
            }

        }
        public async Task<NacBankDto> GetNacBankAsync(SourseGetDto dto)
        {//rssUrl ссылка на источник
            try
            {
                HttpClient httpClient = new HttpClient();
                string request = dto.BaseUrl;
                //https://www.nbrb.by/api/exrates/rates/431;//Доллар
                //451 евро 456расия
                HttpResponseMessage response =
                    (await httpClient.GetAsync(request)).EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                JObject jObject = JObject.Parse(responseBody);

                NacBankDto nacbank = new NacBankDto()
                {
                    ID = Guid.NewGuid(),
                    SiteID = dto.ID,
                    EurBuy = jObject[dto.EurBuyNode].Value<decimal>(),
                    RubBuy = jObject[dto.RubBuyNode].Value<decimal>(),
                    UsdBuy = jObject[dto.UsdBuyNode].Value<decimal>(),
                    BankName = dto.BankName,
                };
                return nacbank;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return null;
            }
        }

        public RssCurrencyDto GetCyrrencyPrior(SourseGetDto dto)
        {
            try
            {
                HtmlWeb web = new HtmlWeb();
                HtmlDocument document = web.Load(dto.BaseUrl);
                var eurcourse = document.DocumentNode.SelectSingleNode(dto.EurBuyNode).InnerText;
                var usdcourse = document.DocumentNode.SelectSingleNode(dto.UsdBuyNode).InnerText;
                var rubcourse = document.DocumentNode.SelectSingleNode(dto.RubBuyNode).InnerText;

                Regex regex = new Regex(@"\d+\.*\d*");
                MatchCollection matchesEUR = regex.Matches(eurcourse);
                MatchCollection matchUSD = regex.Matches(usdcourse);
                MatchCollection matchRUB = regex.Matches(rubcourse);

                IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };

                RssCurrencyDto result = new RssCurrencyDto()
                {
                    ID = Guid.NewGuid(),
                    SiteID = dto.ID,
                    EurBuy = decimal.Parse(matchesEUR[17].Value, formatter),
                    EurSell = decimal.Parse(matchesEUR[18].Value, formatter),
                    RubBuy = decimal.Parse(matchRUB[18].Value, formatter),
                    RubSell = decimal.Parse(matchRUB[20].Value, formatter),
                    UsdBuy = decimal.Parse(matchUSD[17].Value, formatter),
                    UsdSell = decimal.Parse(matchUSD[18].Value, formatter),
                    BankName = dto.BankName,
                    CreationDate = DateTime.Now
                };
                return result;
            }
            catch(Exception e)
            {
                _logger.LogError(e, e.Message);
                return null;
            }


        }


    }
}
