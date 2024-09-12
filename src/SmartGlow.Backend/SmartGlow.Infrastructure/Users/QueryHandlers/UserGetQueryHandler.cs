using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartGlow.Application.Users.Models;
using SmartGlow.Application.Users.Queries;
using SmartGlow.Application.Users.Services;
using SmartGlow.Domain.Common.Queries;

namespace SmartGlow.Infrastructure.Users.QueryHandlers;

public class UserGetQueryHandler(IUserService userService, IMapper mapper) : IQueryHandler<UserGetQuery, ICollection<UserDto>>
{
    public async Task<ICollection<UserDto>> Handle(UserGetQuery request, CancellationToken cancellationToken)
    {
        var matchedClients =  await userService.Get(request.Filter, new QueryOptions() { TrackingMode = QueryTrackingMode.AsNoTracking }).ToListAsync(cancellationToken);
        
        return mapper.Map<ICollection<UserDto>>(matchedClients);
    }
}