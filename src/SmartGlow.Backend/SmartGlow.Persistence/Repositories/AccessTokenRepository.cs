using SmartGlow.Domain.Common.Commands;
using SmartGlow.Domain.Entities;
using SmartGlow.Persistence.Caching.Brokers;
using SmartGlow.Persistence.Caching.Models;
using SmartGlow.Persistence.Repositories.Interfaces;

namespace SmartGlow.Persistence.Repositories;

public class AccessTokenRepository(ICacheBroker cacheBroker) : IAccessTokenRepository
{
    public ValueTask<AccessToken?> GetByIdAsync(Guid tokenId, CancellationToken cancellationToken = default)
    {
        return cacheBroker.GetAsync<AccessToken>(tokenId.ToString(), cancellationToken);
    }

    public async ValueTask<AccessToken> CreateAsync(AccessToken accessToken, CommandOptions commandOptions,  CancellationToken cancellationToken = default)
    {
        var cacheEntryOptions = new CacheEntryOptions(accessToken.ExpiryTime - DateTimeOffset.UtcNow, null);
        await cacheBroker.SetAsync(accessToken.Id.ToString(), accessToken, cacheEntryOptions, cancellationToken);
        
        return accessToken;
    }

    public async ValueTask<AccessToken> UpdateAsync(AccessToken accessToken, CancellationToken cancellationToken = default)
    {
        // Update cache entry with expiration based on AccessToken's ExpiryTime.
        var cacheEntryOptions = new CacheEntryOptions(accessToken.ExpiryTime - DateTimeOffset.UtcNow, null);
        await cacheBroker.SetAsync(accessToken.Id.ToString(), accessToken, cacheEntryOptions, cancellationToken);

        return accessToken;
    }

    public async ValueTask<AccessToken?> DeleteByIdAsync(Guid tokenId, CancellationToken cancellationToken = default)
    {
        var foundAccessToken = await cacheBroker.GetAsync<AccessToken>(tokenId.ToString(), cancellationToken);
        await cacheBroker.DeleteAsync(tokenId.ToString(), cancellationToken);
        
        return foundAccessToken;
    }
}