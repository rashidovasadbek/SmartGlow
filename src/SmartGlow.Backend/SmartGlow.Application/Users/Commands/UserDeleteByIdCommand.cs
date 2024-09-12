using SmartGlow.Domain.Common.Commands;

namespace SmartGlow.Application.Users.Commands;

/// <summary>
/// Represents a command to delete a client by their unique identifier.
/// </summary>
public record UserDeleteByIdCommand: ICommand<bool>
{
    /// <summary>
    /// Gets the unique identifier of the client to delete.
    /// </summary>
    public Guid UserId { get; init; }
}