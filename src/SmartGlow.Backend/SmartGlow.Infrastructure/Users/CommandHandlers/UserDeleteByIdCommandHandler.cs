using SmartGlow.Application.Users.Commands;
using SmartGlow.Application.Users.Services;
using SmartGlow.Domain.Common.Commands;

namespace SmartGlow.Infrastructure.Users.CommandHandlers;

public class UserDeleteByIdCommandHandler(IUserService userService) : ICommandHandler<UserDeleteByIdCommand, bool>
{
    public async Task<bool> Handle(UserDeleteByIdCommand request, CancellationToken cancellationToken)
    {
        await userService.DeleteByIdAsync(request.UserId, cancellationToken: cancellationToken);
        return true;
    }
}