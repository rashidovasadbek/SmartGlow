namespace SmartGlow.Domain.Common.Entities;

public interface IAuditableEntity
{
    /// <summary>
    /// Gets or sets the date and time when the entity was created
    /// </summary>
    public DateTimeOffset  CreatedTime { get; set; }
    
    /// <summary>
    /// Gets or sets the date and time when the entity was updated
    /// Can be null if the entity has never been modified
    /// </summary>
    public DateTimeOffset? ModifiedTime { get; set; }
}