using AutoMapper;
using ExchangeRate.Core.DTOs;
using ExchangeRate.Data.Entities;

namespace ExchangeRate.Mappers
{
    //IMapper<Product, ProductDetailsModel>
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            //CreateMap<Comment, CommentsDTO>()
            //.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name));


        }
    }
}
