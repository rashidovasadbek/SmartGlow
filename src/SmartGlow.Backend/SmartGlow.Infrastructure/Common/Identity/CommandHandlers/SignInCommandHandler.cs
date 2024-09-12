using System.Security.Authentication;
using SmartGlow.Application.Common.Identity.Commands;
using SmartGlow.Application.Common.Identity.Services;
using SmartGlow.Domain.Common.Commands;
using SmartGlow.Domain.Entities;

namespace SmartGlow.Infrastructure.Common.Identity.CommandHandlers;

public class SignInCommandHandler(IAuthService authService) : ICommandHandler<SignInCommand, (AccessToken accessToken, RefreshToken refreshToken)>
{
    public async Task<(AccessToken accessToken, RefreshToken refreshToken)> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        if (request.SignInDetails is not null)
            return await authService.SignInUsernameAsync(request.SignInDetails, cancellationToken);
        
        throw new AuthenticationException("Invalid sign in request");
    }
}