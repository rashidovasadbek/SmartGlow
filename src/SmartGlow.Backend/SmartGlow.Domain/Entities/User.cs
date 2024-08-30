using SmartGlow.Domain.Common.Entities;
using SmartGlow.Domain.Enums;

namespace SmartGlow.Domain.Entities;

public class User : Entity
{
    /// <summary>
    /// Gets or sets user first name
    /// </summary>
    public string FirstName { get; set; } = default!;

    /// <summary>
    /// Gets or sets user last name
    /// </summary>
    public string LastName { get; set; } = default!;

    /// <summary>
    /// Gets or sets user phone number
    /// </summary>
    public string PhoneNumber { get; set; } = default!;

    /// <summary>
    /// gets or sets streets attached to the user
    /// </summary>
    public IList<string> AttachedStreets { get; set; } = [];
    
    /// <summary>
    /// gets or sets User role
    /// </summary>
    public RoleType Role { get; set; }

    /// <summary>
    /// gets or sets username
    /// </summary>
    public string UserName { get; set; } = default!;

    /// <summary>
    /// Gets or sets password
    /// </summary>
    public string Password { get; set; } = default!;
}