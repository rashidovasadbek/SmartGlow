using Microsoft.EntityFrameworkCore;
using SmartGlow.Application.Common.Identity.Events;
using SmartGlow.Application.Common.Identity.Services;
using SmartGlow.Application.Users.Services;
using SmartGlow.Domain.Common.Queries;
using SmartGlow.Domain.Entities;
using SmartGlow.Persistence.Repositories.Interfaces;

namespace SmartGlow.Infrastructure.Common.Identity.Services;

public class AccountService(IUserService userService, IUserRepository userRepository): IAccountService 
{
    public async ValueTask<User> GetUserByUsernameAsync(string username, QueryOptions queryOptions,
        CancellationToken cancellationToken = default)
    {
        return await userRepository
            .Get(queryOptions: new QueryOptions{ TrackingMode = QueryTrackingMode.AsNoTracking})
            .FirstOrDefaultAsync(client => client.UserName == username, cancellationToken: cancellationToken);

    }

    public async ValueTask<User> CreateUserAsync(User user, CancellationToken cancellationToken = default)
    {
        var createdUser = await userService.CreateAsync(user, cancellationToken: cancellationToken);

        return createdUser;
    }
}