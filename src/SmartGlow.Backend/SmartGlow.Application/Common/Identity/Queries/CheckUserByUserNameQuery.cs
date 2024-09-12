using SmartGlow.Domain.Common.Queries;

namespace SmartGlow.Application.Common.Identity.Queries;

/// <summary>
/// Represents user checking query that returns user's firstname if exists
/// </summary>
public class CheckUserByUserNameQuery : IQuery<string>
{
    /// <summary>
    /// Gets user bu username
    /// </summary>
    public string  UserName { get; init;} = default!;
}