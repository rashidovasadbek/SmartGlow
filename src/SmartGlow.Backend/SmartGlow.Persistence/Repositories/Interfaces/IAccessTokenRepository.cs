using SmartGlow.Domain.Entities;

namespace SmartGlow.Persistence.Repositories.Interfaces;

/// <summary>
/// Defines access token repository functionality.
/// </summary>
public interface IAccessTokenRepository
{
    /// <summary>
    /// Gets a single access token by Id.
    /// </summary>
    /// <param name="tokenId">The Id of the access token.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>AccessToken if found, otherwise null.</returns>
    ValueTask<AccessToken?> GetByIdAsync(Guid tokenId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates an access token.
    /// </summary>
    /// <param name="accessToken">The access token to be created.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>Created access token.</returns>
    ValueTask<AccessToken> CreateAsync(AccessToken accessToken,  CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing access token.
    /// </summary>
    /// <param name="accessToken">The access token to be updated.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>Updated access token.</returns>
    ValueTask<AccessToken> UpdateAsync(AccessToken accessToken, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an access token by Id.
    /// </summary>
    /// <param name="tokenId">The Id of the access token to be deleted.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>AccessToken if found and deleted, otherwise null.</returns>
    ValueTask<AccessToken?> DeleteByIdAsync(Guid tokenId, CancellationToken cancellationToken = default);
}