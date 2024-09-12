using SmartGlow.Domain.Common.Commands;

namespace SmartGlow.Application.Common.Identity.Commands;

public class SignOutCommand : ICommand<bool>
{
    /// <summary>
    /// User's refresh token
    /// </summary>
    public string RefreshToken { get; set; } = default!;
}