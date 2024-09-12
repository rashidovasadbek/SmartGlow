using SmartGlow.Domain.Common.Entities;

namespace SmartGlow.Domain.Entities;

/// <summary>
/// Represents identity refresh token.
/// </summary>
public class RefreshToken : Entity
{
    /// <summary>
    /// Gets or sets the user Id
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the refresh token value
    /// </summary>
    public string Token { get; set; } = default!;

    /// <summary>
    /// Gets or sets the expiration time of the refresh token.
    /// </summary>
    public DateTimeOffset ExpiryTime { get; set; }

    /// <summary>
    /// Gets or sets if it is enable to extend expiration time of the refresh token
    /// </summary>
    public bool EnableExtendedExpiryTime { get; set; }
}