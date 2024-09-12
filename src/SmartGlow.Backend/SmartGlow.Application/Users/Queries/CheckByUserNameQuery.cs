using SmartGlow.Domain.Common.Queries;

namespace SmartGlow.Application.Users.Queries;

/// <summary>
/// Represents user checking query that returns user's firstname if exists
/// </summary>
public class CheckByUserNameQuery : IQuery<string?>
{
    /// <summary>
    /// Gets user email address
    /// </summary>
    public string Username { get; set; } = default!;
}