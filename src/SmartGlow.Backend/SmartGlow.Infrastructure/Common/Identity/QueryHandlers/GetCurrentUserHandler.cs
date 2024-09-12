using System.Security.Authentication;
using SmartGlow.Application.Common.Identity.Queries;
using SmartGlow.Application.Common.Identity.Services;
using SmartGlow.Application.Users.Services;
using SmartGlow.Domain.Brokers;
using SmartGlow.Domain.Common.Queries;
using SmartGlow.Domain.Entities;

namespace SmartGlow.Infrastructure.Common.Identity.QueryHandlers;

public class GetCurrentUserHandler(IRequestUserContextProvider requestUserContextProvider, IUserService userService)
    : IQueryHandler<GetCurrentUserQuery, User>    
{
    public async Task<User> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var userId = requestUserContextProvider.GetUserId();

        var foundUser = await userService.GetByIdAsync(userId, cancellationToken: cancellationToken);
        
        if(foundUser is null)
            throw new AuthenticationException("Current logged in user not found");
        
        return foundUser;
    }
}