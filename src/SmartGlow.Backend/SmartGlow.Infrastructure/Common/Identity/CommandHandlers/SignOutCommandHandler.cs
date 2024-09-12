using SmartGlow.Application.Common.Identity.Commands;
using SmartGlow.Application.Common.Identity.Services;
using SmartGlow.Domain.Brokers;
using SmartGlow.Domain.Common.Commands;
using SmartGlow.Domain.Extensions;

namespace SmartGlow.Infrastructure.Common.Identity.CommandHandlers;

public class SignOutCommandHandler(IRequestUserContextProvider requestUserContextProvider, IAuthService authService) : ICommandHandler<SignOutCommand, bool>
{
    public async Task<bool> Handle(SignOutCommand request, CancellationToken cancellationToken)
    {
        var accessToken = requestUserContextProvider.GetAccessToken();
        var signOutTask = () => authService.SignOutAsync(accessToken, request.RefreshToken, cancellationToken);
        var signOutResult = await signOutTask.GetValueAsync();
        
        return signOutResult.IsSuccess;
    }
}