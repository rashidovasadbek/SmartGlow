namespace SmartGlow.Application.Common.Identity.Models;

/// <summary>
/// Represents the SignIN details by username
/// </summary>
public class SignInDetails
{
    /// <summary>
    /// Gets or sets the username
    /// </summary>
    public string Username { get; set; } = default!;
    
    /// <summary>
    /// Gets sign in password
    /// </summary>
    public string Password { get; set; } = default!;
    
    /// <summary>
    /// Gets sign extended active time
    /// </summary>
    public bool RememberMe { get; set; }
}