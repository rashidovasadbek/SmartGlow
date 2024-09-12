using AutoMapper;
using SmartGlow.Application.Common.Identity.Models;
using SmartGlow.Application.Users.Models;
using SmartGlow.Domain.Entities;

namespace SmartGlow.Application.Users.Mappers;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<SignInDetails, User>();
        CreateMap<SignUpDetails, User>();
    }
}