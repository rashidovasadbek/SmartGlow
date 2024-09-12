using Microsoft.EntityFrameworkCore;
using SmartGlow.Application.Common.Identity.Queries;
using SmartGlow.Application.Common.Identity.Services;
using SmartGlow.Application.Users.Services;
using SmartGlow.Domain.Common.Queries;

namespace SmartGlow.Infrastructure.Common.Identity.QueryHandlers;

public class CheckUserExistenceQueryHandler(IUserService userService) : IQueryHandler<CheckUserByUserNameQuery, string>
{
    public Task<string> Handle(CheckUserByUserNameQuery request, CancellationToken cancellationToken)
    {
        var userName = userService
            .Get(user =>  user.UserName == request.UserName,
                new QueryOptions
                {
                    TrackingMode = QueryTrackingMode.AsNoTracking
                }
            )
            .Select(user => user.UserName)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        
        return userName;
    }
}