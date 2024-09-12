using SmartGlow.Domain.Entities;

namespace SmartGlow.Domain.Extensions;

/// <summary>
/// Contains extensions for cache key generation
/// </summary>
public static class CacheKeyExtensions
{
    #region General Cache Key

    /// <summary>
    /// Generates cache key for check by ID operation
    /// </summary>
    /// <typeparam name="TModel">The type of the model for which the cache key is being generated.</typeparam>
    /// <param name="id">The ID of the entity</param>
    /// <returns>Generated cache key</returns>
    public static string CheckById<TModel>(Guid id) => $"{typeof(TModel).Name}-{nameof(CheckById)}-{id}";

    #endregion

    #region Identity Cache Key

    /// <summary>
    /// Generates cache key for check by phone number operation
    /// </summary>
    /// <param name="phoneNumber">The phone number of the entity</param>
    /// <returns>Generated cache key</returns>
    public static string CheckByPhoneNumber(string phoneNumber) => $"{nameof(CheckByPhoneNumber)} - {phoneNumber}";
    
    /// <summary>
    /// Generates cache key for getting phone number by ID operation
    /// </summary>
    /// <param name="id">The ID of the user</param>
    /// <returns>Generated cache key</returns>
    public static string GetPhoneNumberBy(Guid id) => $"{nameof(GetPhoneNumberBy)}-{id}";
    
    /// <summary>
    /// Generates cache key for getting ID by phone number operation
    /// </summary>
    /// <param name="phoneNumber">The phone number of the user</param>
    /// <returns>Generated cache key</returns>
    public static string GetIdByPhoneNumber(string phoneNumber) => $"{nameof(GetIdByPhoneNumber)}-{phoneNumber}";
    
    /// <summary>
    /// Generates cache key for refresh token
    /// </summary>
    /// <param name="token">The token of the refresh token</param>
    /// <returns>Generated cache key</returns>
    public static string RefreshToken(string token) => $"{nameof(RefreshToken)}-{token}";
    
    /// <summary>
    /// Generates cache key for getting system user ID operation
    /// </summary>
    /// <returns>Generated cache key</returns>
    public static string GetSystemUserId() => nameof(GetSystemUserId);
    
    /// <summary>
    /// Generates cache key for getting refresh token by value operation
    /// </summary>
    /// <param name="refreshTokenValue">The value of the refresh token</param>
    /// <returns>Generated cache key</returns>
    public static string GetRefreshTokenByValue(RefreshToken refreshTokenValue) => $"{nameof(RefreshToken)}-{refreshTokenValue}";

    /// <summary>
    /// Generates cache key for removing refresh token by value operation
    /// </summary>
    /// <param name="refreshTokenValue">The value of the refresh token</param>
    /// <returns>Generated cache key</returns>
    public static string RemoveRefreshTokenByValue(string refreshTokenValue) => $"{nameof(RefreshToken)}-{refreshTokenValue}";

    #endregion
}