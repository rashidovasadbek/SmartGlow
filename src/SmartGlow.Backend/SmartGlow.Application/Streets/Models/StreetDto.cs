namespace SmartGlow.Application.Streets.Models;

public class StreetDto
{
    /// <summary>
    /// Gets or sets the unique identifier
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Gets or sets the unique user identifier
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the  name of the street
    /// </summary>
    public string StreetName { get; set; } = default!;
    
    /// <summary>
    /// Gets or sets the latitude of street
    /// </summary>
    public double Latitude { get; set; }
    
    /// <summary>
    /// Gets or sets the longitude of street
    /// </summary>
    public double Longitude { get; set; }
}