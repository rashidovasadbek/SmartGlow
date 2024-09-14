using SmartGlow.Domain.Common.Entities;

namespace SmartGlow.Domain.Entities;

public class Street : AuditableEntity
{
    /// <summary>
    /// Gets or sets the name of the street
    /// </summary>
    public string StreetName { get; set; } = default!;
    
    /// <summary>
    /// Gets or sets the Latitude  coordinate
    /// </summary>
    public double Latitude { get; set; } 
    
    /// <summary>
    /// Gets or sets the longitude  coordinate
    /// </summary>
    public double Longitude { get; set; }
    
    /// <summary>
    /// The unique identifier of the user associated with this street.
    /// </summary>
    public Guid UserId { get; set; } = default!;
    
    /// <summary>
    /// Gets or sets the User
    /// </summary>
    public User User { get; set; } = default!;
}