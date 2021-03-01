using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using AutoMapper;

namespace Api.CrossCutting.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDto, UserEntity>()
            .ReverseMap();

            CreateMap<UserDtoCreate, UserEntity>()
            .ReverseMap();

            CreateMap<UserDtoUpdate, UserEntity>()
            .ReverseMap();
        }
    }
}
