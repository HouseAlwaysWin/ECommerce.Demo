using API.Domain.DTO;
using AutoMapper;
using ECommerce.Demo.API.Domain.DTO;
using ECommerce.Demo.API.Domain.Entities;

namespace API.Extensions {
    public class AutoMapperProfiles : Profile {
        public AutoMapperProfiles () {
            CreateMap<UserRegisterDto, User> ();
            CreateMap<UserLoginDto, UserDetailDto> ();
        }
    }
}