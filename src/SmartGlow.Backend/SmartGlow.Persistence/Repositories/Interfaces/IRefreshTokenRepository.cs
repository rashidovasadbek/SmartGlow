using SmartGlow.Domain.Entities;

namespace SmartGlow.Persistence.Repositories.Interfaces;

/// <summary>
/// Defines refresh token repository functionality.
/// </summary>
public interface IRefreshTokenRepository
{
    /// <summary>
    /// Creates an refresh token.
    /// </summary>
    /// <param name="refreshToken">The access token to be created.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>Created access token.</returns>
    ValueTask<RefreshToken> CreateAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets refresh token by value
    /// </summary>
    /// <param name="refreshTokenValue">Refresh token value</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>Refresh token if found, otherwise null</returns>
    ValueTask<RefreshToken?> GetByValueAsync(RefreshToken refreshTokenValue, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an refresh token token by value
    /// </summary>
    /// <param name="refreshTokenValue">The value of refresh token.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    ValueTask RemoveAsync(string refreshTokenValue, CancellationToken cancellationToken = default);
}