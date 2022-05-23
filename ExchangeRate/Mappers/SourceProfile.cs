using AutoMapper;
using ExchangeRate.Core.DTOs;
using ExchangeRate.Core.Interfaces.Data;
using ExchangeRate.Data.Entities;

namespace ExchangeRate.Mappers
{
    public class SourceProfile : Profile
    {
        public SourceProfile()
        {
            CreateMap<Source, RssUrlsFromSourceDto>()
                .ForMember(dto => dto.SourceId,
                    opt => opt.MapFrom(source => source.ID))
                .ForMember(dto => dto.EurBuyNode,
                    opt => opt.MapFrom(source => source.EurBuyNode))
                .ForMember(dto => dto.EurSellNode,
                    opt => opt.MapFrom(source => source.EurSellNode))
                 .ForMember(dto => dto.RubBuyNode,
                    opt => opt.MapFrom(source => source.RubBuyNode))
                .ForMember(dto => dto.RubSellNode,
                    opt => opt.MapFrom(source => source.RubSellNode))
                 .ForMember(dto => dto.UsdBuyNode,
                    opt => opt.MapFrom(source => source.UsdBuyNode))
                .ForMember(dto => dto.UsdSellNode,
                    opt => opt.MapFrom(source => source.UsdSellNode));


            CreateMap<RssCurrencyDto, Currency>()
                .ForMember(dto => dto.ID,
                    opt => opt.MapFrom(source => source.ID))
                .ForMember(dto => dto.SiteID,
                    opt => opt.MapFrom(source => source.SiteID))
                 .ForMember(dto => dto.EurBuy,
                    opt => opt.MapFrom(source => source.EurBuy))
                 .ForMember(dto => dto.EurSell,
                    opt => opt.MapFrom(source => source.EurSell))
                 .ForMember(dto => dto.RubBuy,
                    opt => opt.MapFrom(source => source.RubBuy))
                 .ForMember(dto => dto.RubSell,
                    opt => opt.MapFrom(source => source.RubSell))
                 .ForMember(dto => dto.UsdBuy,
                    opt => opt.MapFrom(source => source.UsdBuy))
                 .ForMember(dto => dto.UsdSell,
                    opt => opt.MapFrom(source => source.UsdSell))
                .ForMember(dto => dto.BankName,
                    opt => opt.MapFrom(source => source.BankName))
                .ForMember(dto => dto.CreationDate,
                opt => opt.MapFrom(source => source.CreationDate));


            CreateMap<NacBankDto, Currency>()
                .ForMember(dto => dto.SiteID,
                opt => opt.MapFrom(source => source.ID))
                 .ForMember(dto => dto.EurBuy,
                    opt => opt.MapFrom(source => source.EurBuy))
                 .ForMember(dto => dto.RubBuy,
                    opt => opt.MapFrom(source => source.RubBuy))
                 .ForMember(dto => dto.UsdBuy,
                    opt => opt.MapFrom(source => source.UsdBuy))
                .ForMember(dto => dto.BankName,
                    opt => opt.MapFrom(source => source.BankName));

            CreateMap<Source, SourseGetDto>()
                .ForMember(dto => dto.ID,
                opt => opt.MapFrom(source => source.ID))
                .ForMember(dto => dto.EurBuyNode,
                    opt => opt.MapFrom(source => source.EurBuyNode))
                .ForMember(dto => dto.EurSellNode,
                    opt => opt.MapFrom(source => source.EurSellNode))
                 .ForMember(dto => dto.RubBuyNode,
                    opt => opt.MapFrom(source => source.RubBuyNode))
                .ForMember(dto => dto.RubSellNode,
                    opt => opt.MapFrom(source => source.RubSellNode))
                 .ForMember(dto => dto.UsdBuyNode,
                    opt => opt.MapFrom(source => source.UsdBuyNode))
                .ForMember(dto => dto.UsdSellNode,
                    opt => opt.MapFrom(source => source.UsdSellNode))
                .ForMember(dto => dto.BaseUrl,
                opt => opt.MapFrom(source => source.BaseUrl))
                .ForMember(dto => dto.BankName,
                opt => opt.MapFrom(source => GetName(source.BaseUrl)));

        }

        private string GetName(string url)
        {//получаем из ссылки на сайт имя банка
            string value = url.Substring(url.IndexOf('/'));
            value = value.Replace(".by", "");
            value = value.Replace("/", "");
            value = value.Replace("www.", "");
            if (value.IndexOf("bank") == -1)
                value = value + "bank";
            return value;
        }


    }
}
