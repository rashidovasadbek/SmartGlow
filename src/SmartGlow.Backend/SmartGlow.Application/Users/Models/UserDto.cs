namespace SmartGlow.Application.Users.Models;

/// <summary>
/// Data transfer object (DTO) representing a client.
/// </summary>
public class UserDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the listing.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Gets or sets the first name of the user.
    /// </summary>
    public string FirstName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the last name of the user.
    /// </summary>
    public string LastName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the email address of the user.
    /// </summary>
    public string Username { get; set; } = default!;
    
    /// <summary>
    /// Gets or sets the phoneNumber of the user.
    /// </summary>
    public string PhoneNumber { get; set; } = default!;
    
    /// <summary>
    /// Gets or sets the attachedStreets of the user;
    /// </summary>
    public IList<string> AttachedStreets { get; set; } = [];
}