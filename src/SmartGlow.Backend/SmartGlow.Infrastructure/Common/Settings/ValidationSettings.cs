namespace SmartGlow.Infrastructure.Common.Settings;

/// <summary>
/// Represents the settings for validation.
/// </summary>
public sealed record ValidationSettings
{
    /// <summary>
    /// Gets the regular expression pattern for validating names.
    /// </summary>
    public string PersonNameRegexPattern { get; init; } = default!; 
    
    /// <summary>
    /// Gets the regular expression pattern for validating phone numbers.
    /// </summary>
    public string PhoneNumberRegexPattern { get; init; } = default!;
    
    /// <summary>
    /// Gets the regular expression pattern for validating password.
    /// </summary>
    public string PasswordRegexPattern { get; init; } = default!;
}