using SmartGlow.Domain.Entities;
using SmartGlow.Domain.Extensions;
using SmartGlow.Persistence.Caching.Brokers;
using SmartGlow.Persistence.Caching.Models;
using SmartGlow.Persistence.Repositories.Interfaces;

namespace SmartGlow.Persistence.Repositories;

public class RefreshTokenRepository(ICacheBroker cacheBroker) : IRefreshTokenRepository
{
    public async ValueTask<RefreshToken> CreateAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
    {
        var cacheEntryOptions = new CacheEntryOptions(null, refreshToken.ExpiryTime - DateTimeOffset.UtcNow);
        await cacheBroker.SetAsync(CacheKeyExtensions.RefreshToken(refreshToken.Token), refreshToken, cacheEntryOptions,
            cancellationToken);
        
        return refreshToken;
    }

    public ValueTask<RefreshToken?> GetByValueAsync(RefreshToken refreshTokenValue, CancellationToken cancellationToken = default)
    {
        return cacheBroker.GetAsync<RefreshToken>(CacheKeyExtensions.GetRefreshTokenByValue(refreshTokenValue),
            cancellationToken);
    }

    public ValueTask RemoveAsync(string refreshTokenValue, CancellationToken cancellationToken = default)
    {
        return cacheBroker.DeleteAsync(CacheKeyExtensions.RefreshToken(refreshTokenValue), cancellationToken);
    }
}