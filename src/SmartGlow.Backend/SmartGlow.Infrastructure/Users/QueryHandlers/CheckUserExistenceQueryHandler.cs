using Microsoft.EntityFrameworkCore;
using SmartGlow.Application.Users.Queries;
using SmartGlow.Application.Users.Services;
using SmartGlow.Domain.Common.Queries;

namespace SmartGlow.Infrastructure.Users.QueryHandlers;

public class CheckUserExistenceQueryHandler(IUserService userService) : IQueryHandler<CheckByUserNameQuery, string>
{
    public Task<string> Handle(CheckByUserNameQuery request, CancellationToken cancellationToken)
    {
        var clientFirstName = userService
            .Get(
                client => client.UserName == request.Username,
                new QueryOptions
                {
                    TrackingMode = QueryTrackingMode.AsNoTracking
                }
            )
            .Select(client => client.FirstName)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        return clientFirstName;
    }
}