using SmartGlow.Application.Common.Identity.Models;
using SmartGlow.Domain.Common.Commands;
using SmartGlow.Domain.Entities;

namespace SmartGlow.Application.Common.Identity.Commands;

public class SignInCommand : ICommand<(AccessToken accessToken, RefreshToken refreshToken)>
{
    /// <summary>
    /// Sign in by username
    /// </summary>
    public SignInDetails SignInDetails { get; set; } = default!;
}