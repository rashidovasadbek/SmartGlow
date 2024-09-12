namespace SmartGlow.Application.Common.Identity.Models;

public class SignUpDetails
{
    /// <summary>
    /// Gets or sets first name of the sign up details
    /// </summary>
    public string FirstName { get; set; } = default!;

    /// <summary>
    /// Gets or sets last name of the sign up details
    /// </summary>
    public string LastName { get; set; } = default!;
    
    /// <summary>
    /// Gets or sets username of the sign up details
    /// </summary>
    public string UserName { get; set; } = default!;
    
    /// <summary>
    /// Gets or sets attachedStreets of the sign up details
    /// </summary>
    public IList<string> AttachedStreets { get; set; } = [];
   
    /// <summary>
    /// PhoneNumber of the sign up details
    /// </summary>
    public string PhoneNumber { get; set; } = default!;
    
    /// <summary>
    /// Password of the sign up details
    /// </summary>
    public string Password { get; set; } = default!;
}