namespace SmartGlow.Domain.Common.Queries;

/// <summary>
/// Represents a options to configure data querying behavior
/// </summary>
public struct QueryOptions()
{
    /// <summary>
    /// Gets or sets change tracking behavior for query result
    /// </summary>
    public QueryTrackingMode TrackingMode { get; set; }

    public QueryOptions(QueryTrackingMode trackingMode) : this() => TrackingMode = trackingMode;
}