using SmartGlow.Application.Users.Models;
using SmartGlow.Domain.Common.Queries;

namespace SmartGlow.Application.Users.Queries;

/// <summary>
/// Represents a command to retrieve a collection of clients based on specified filtering criteria.
/// </summary>
public record UserGetQuery : IQuery<ICollection<UserDto>>
{
    public UserFilter Filter { get; init; } = default!;
}