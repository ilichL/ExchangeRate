
using AutoMapper;
using ExchangeRate.Core.DTOs;
using ExchangeRate.Data.Entities;
using ExchangeRate.Models.Currency;
using System.ServiceModel.Syndication;

namespace ExchangeRate.Mappers
{
    public class CurrencyProfile : Profile
    {//IMapper<Product, ProductDetailsModel>
        public CurrencyProfile()
        {
            CreateMap<Currency, CurrencyDetailsModel>();
            //CreateMap<Currency, CurrencyDTO>()
                //.ForMember(dest => dest, opt => opt.MapFrom(src => src.Site.Name));
            //приводим тип ProductDTO к типу Product и забирем с сайта имя сайта
            CreateMap<CurrencyDTO, CurrencyListView>();//??
            CreateMap<CurrencyDTO, CurrencyDetailsModel>();
            //переделать(Удалить?)

            // CreateMap<SyndicationItem, NacBankDto>()
            //   .ForMember(dto => dto.Cur_OfficialRate, opt => opt.MapFrom(item => item.))
            //CreateMap<>

            CreateMap<Currency, CurrencyTableViewModel>()
                    .ForMember(opt => opt.UsdBuy,
                    opt => opt.MapFrom(source => source.UsdBuy))
                   .ForMember(opt => opt.UsdSell,
                    opt => opt.MapFrom(source => source.UsdSell))
                   .ForMember(opt => opt.EurBuy,
                    opt => opt.MapFrom(source => source.EurBuy))
                   .ForMember(opt => opt.EurSell,
                    opt => opt.MapFrom(source => source.EurSell))
                   .ForMember(opt => opt.RubBuy,
                    opt => opt.MapFrom(source => source.RubBuy))
                    .ForMember(opt => opt.RubSell,
                    opt => opt.MapFrom(source => source.RubSell))
                    .ForMember(opt => opt.BankName,
                    opt => opt.MapFrom(source => source.BankName))
                    .ForMember(opt => opt.CreationDate,
                    opt => opt.MapFrom(source => source.CreationDate));

        }
    }
}
