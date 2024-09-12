namespace SmartGlow.Domain.Common.Queries;

/// <summary>
/// Represents query tracking modes for query result tracking
/// </summary>
public enum QueryTrackingMode
{
    /// <summary>
    /// Specifies that query result changes should be tracked
    /// </summary>
    AsTracking,
    
    /// <summary>
    /// Specifies that query result changes should not be tracked
    /// </summary>
    AsNoTracking,
    
    /// <summary>
    /// Specifies that query result changes should not be tracked but identity resolution should be performed
    /// </summary>
    AsNoTrackingWithIdentityResolution
}