using AutoMapper;
using SmartGlow.Application.Common.Identity.Models;
using SmartGlow.Domain.Entities;

namespace SmartGlow.Application.Users.Mappers;

public class AccessTokenMapper : Profile
{
    public AccessTokenMapper()
    {
        CreateMap<AccessToken, AccessTokenDto>().ReverseMap();
    }
}