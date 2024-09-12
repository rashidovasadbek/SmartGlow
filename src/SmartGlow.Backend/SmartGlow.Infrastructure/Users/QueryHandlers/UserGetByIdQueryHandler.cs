using AutoMapper;
using SmartGlow.Application.Users.Models;
using SmartGlow.Application.Users.Queries;
using SmartGlow.Application.Users.Services;
using SmartGlow.Domain.Common.Queries;
using SmartGlow.Persistence.Repositories.Interfaces;

namespace SmartGlow.Infrastructure.Users.QueryHandlers;

public class UserGetByIdQueryHandler(IUserService userService, IMapper mapper) : IQueryHandler<UserGetByIdQuery, UserDto?>
{
    public async Task<UserDto?> Handle(UserGetByIdQuery request, CancellationToken cancellationToken)
    {
        var foundUser = await userService.GetByIdAsync(
            request.UserId,
            new QueryOptions()
            {
                TrackingMode = QueryTrackingMode.AsNoTracking
            },
            cancellationToken
        );
        
        return mapper.Map<UserDto>(foundUser);
    }
}