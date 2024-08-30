namespace SmartGlow.Domain.Common.Entities;

/// <summary>
/// Defines an entity within the system
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity. This identifier should be unique globally unique within the system
    /// </summary>
    public Guid Id { get; set; }
}