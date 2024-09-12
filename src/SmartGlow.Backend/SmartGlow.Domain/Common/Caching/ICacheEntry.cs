using System.Text.Json.Serialization;

namespace SmartGlow.Domain.Common.Caching;

/// <summary>
/// Defines cache entry properties.
/// </summary>
public interface ICacheEntry
{
    /// <summary>
    /// Gets or sets the cache key.
    /// </summary>
    [JsonIgnore]
    string CacheKey { get; }
}