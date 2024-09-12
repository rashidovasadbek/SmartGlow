namespace SmartGlow.Application.Common.Identity.Models;

/// <summary>
/// Represents identity data transfer object.
/// </summary>
public sealed record IdentitySecurityTokenDto
{
    /// <summary>
    /// Gets or sets access token string
    /// </summary>
    public string AccessToken { get; init; } = default!;

    /// <summary>
    /// Gets or sets refresh token string
    /// </summary>
    public string RefreshToken { get; init; } = default!;
}