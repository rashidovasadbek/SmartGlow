namespace SmartGlow.Domain.Common.Entities;

/// <summary>
/// Represents an entity that supports soft deletion, inheriting properties from IEntity
/// </summary>
public interface ISoftDeletedEntity : IEntity
{
    /// <summary>
    ///Gets or sets a value indicating whether the entity is soft deleted
    /// </summary>
    public bool IsDeleted { get; set; }
    
    /// <summary>
    /// Get or sets date and time when the entity was soft deleted. This value will be null if the entity had not been soft deleted
    /// </summary>
    public DateTimeOffset? DeletedTime { get; set; }  
}