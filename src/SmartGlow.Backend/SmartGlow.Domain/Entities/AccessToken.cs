using SmartGlow.Domain.Common.Entities;

/// <summary>
/// Represents identity access token.
/// </summary>
namespace SmartGlow.Domain.Entities;

public sealed class AccessToken : Entity
{
    public AccessToken()
    {
        
    }

    public AccessToken(Guid id, Guid userId, string token, DateTimeOffset expiryTime, bool isRevoked)
    {
        Id = id;
        UserId = userId;
        Token = token;
        ExpiryTime = expiryTime;
        IsRevoked = isRevoked;
    }
    /// <summary>
    /// Gets or sets the user Id.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the access token value.
    /// </summary>
    public string Token { get; set; } = default!;

    /// <summary>
    /// Gets or sets the expiration time of the access token.
    /// </summary>
    public DateTimeOffset ExpiryTime { get; set; }

    /// <summary>
    /// Gets or sets a flag indicating whether the access token is revoked.
    /// </summary>
    public bool IsRevoked { get; set; }

}