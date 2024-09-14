using AutoMapper;
using SmartGlow.Application.Common.Identity.Models;
using SmartGlow.Domain.Entities;

namespace SmartGlow.Application.Users.Mappers;

public class IdentityTokenMapper : Profile
{
    public IdentityTokenMapper()
    {
        CreateMap<AccessToken, AccessTokenDto>();

        CreateMap<(AccessToken AccessToken, RefreshToken refreshToken), IdentityTokenDto>()
            .ForMember(dest => dest.AccessToken, opt => opt.MapFrom(src => src.AccessToken.Token))
            .ForMember(dest => dest.RefreshToken, opt => opt.MapFrom(src => src.refreshToken.Token));
    }
}