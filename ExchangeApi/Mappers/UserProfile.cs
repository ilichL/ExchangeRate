using AutoMapper;
using ExchangeRate.Core.DTOs;
using ExchangeRate.Data.Entities;
using System.Linq;

namespace WebApi.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.RoleNames,
                    opt => opt.MapFrom(src => src.UserRoles.Select(role => role.Role.Name)));

        }
    }
}
