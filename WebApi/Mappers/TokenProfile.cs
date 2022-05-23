using AutoMapper;
using ExchangeRate.Core.DTOs;
using ExchangeRate.Data.Entities;
using WebApi.Models.Requests;
using WebApi.Models.Responses;

namespace WebApi.Mappers
{
    public class TokenProfile : Profile
    {
        public TokenProfile()
        {
            CreateMap<AuthenticateRequest, LoginDto>();
            CreateMap<RefreshToken, RefreshTokenDto>();
            CreateMap<RefreshTokenDto, RefreshToken>();
            CreateMap<JwtAuthDto, AuthenticateResponse>();
        }
    }
}
