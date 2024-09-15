using SmartGlow.Domain.Common.Entities;

namespace SmartGlow.Domain.Entities;

public class OnOffTiming : AuditableEntity
{
    /// <summary>
    /// Gets or sets the onTime of the Lumps
    /// </summary>
    public DateTime OnTime { get; set; }
    
    /// <summary>
    /// Gets or sets the OffTime of the Lumps
    /// </summary>
    public DateTime OffTime { get; set; }
    
    /// <summary>
    /// Gets or sets the OnOffTime of the Lumps
    /// </summary>
    public byte OnLights { get; set; }
    
    /// <summary>
    /// Gets or sets the OnOffTime of the Lumps
    /// </summary>
    public byte OffLights { get; set; }
    
    /// <summary>
    /// Gets or sets the LitUnits of the Lumps
    /// </summary>
    public byte LitUnits { get; set; }
    
    /// <summary>
    /// The unique identifier of the street associated with this OnOffTiming.
    /// </summary>
    public Guid StreetId { get; set; }

    /// <summary>
    /// Gets or sets the Street
    /// </summary>
    public Street Street { get; set; } = default!;
}