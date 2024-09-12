using Microsoft.AspNetCore.Http;
using SmartGlow.Domain.Brokers;
using SmartGlow.Domain.Constants;

namespace SmartGlow.Infrastructure.Common.RequestContext.Brokers;

public class RequestUserContextProvider(IHttpContextAccessor httpContextAccessor) : IRequestUserContextProvider
{
    public Guid GetUserId()
    {
        
        if(!IsLoggedIn())
            throw new InvalidOperationException("User is not logged in");
        
        var httpContext = httpContextAccessor.HttpContext;
        var userIdClaim = httpContext!.User.Claims.First(claim => claim.Type == ClaimConstants.UserId).Value;

        return Guid.Parse(userIdClaim);
    }

    public string? GetAccessToken()
    {
        return httpContextAccessor.HttpContext?.Request.Headers.Authorization;
    }

    public bool IsLoggedIn()
    {
        return httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;
    }
}