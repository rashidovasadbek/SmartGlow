using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartGlow.Application.Streets.Models;
using SmartGlow.Application.Streets.Queries;
using SmartGlow.Application.Streets.Services;
using SmartGlow.Domain.Brokers;
using SmartGlow.Domain.Common.Queries;
using SmartGlow.Persistence.Extensions;

namespace SmartGlow.Infrastructure.Streets.QueryHandlers;

public class StreetGetQueryHandler(
    IMapper mapper, 
    IStreetService streetService, 
    IRequestUserContextProvider requestUserContextProvider) : 
    IQueryHandler<StreetGetQuery, ICollection<StreetDto>>
{
    public async Task<ICollection<StreetDto>> Handle(StreetGetQuery request, CancellationToken cancellationToken)
    {
        request.StreetFilter.UserId = requestUserContextProvider.GetUserId();
        var queryOptions = new QueryOptions(QueryTrackingMode.AsNoTracking);
        
        
        var matchedStreets = await (await streetService
            .Get(request.StreetFilter, queryOptions)
            .GetFilteredEntitiesQuery(streetService.Get(), cancellationToken: cancellationToken))
            .Include(street => street.User)
            .ApplyTrackingMode(queryOptions.TrackingMode)
            .ToListAsync(cancellationToken: cancellationToken);

        return mapper.Map<ICollection<StreetDto>>(matchedStreets);
    }
}