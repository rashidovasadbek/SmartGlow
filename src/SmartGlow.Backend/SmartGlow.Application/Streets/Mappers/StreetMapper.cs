using AutoMapper;
using SmartGlow.Application.Streets.Models;
using SmartGlow.Domain.Entities;

namespace SmartGlow.Application.Streets.Mappers;

public class StreetMapper : Profile
{
    public StreetMapper()
    {
        CreateMap<Street, StreetDto>().ReverseMap();
    }
}