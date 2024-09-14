using AutoMapper;
using SmartGlow.Application.Streets.Models;
using SmartGlow.Application.Streets.Queries;
using SmartGlow.Application.Streets.Services;
using SmartGlow.Domain.Common.Queries;

namespace SmartGlow.Infrastructure.Streets.QueryHandlers;

public class StreetGetByIdQueryHandler(IMapper mapper, IStreetService streetService) : IQueryHandler<StreetGetByIdQuery, StreetDto>
{
    public async Task<StreetDto> Handle(StreetGetByIdQuery request, CancellationToken cancellationToken)
    {
        var foundStreet = await streetService.GetByIdAsync(request.StreetId, new QueryOptions()
            {
                TrackingMode = QueryTrackingMode.AsTracking
            },
            cancellationToken);
        
        return mapper.Map<StreetDto>(foundStreet);
    }
}