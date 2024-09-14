using SmartGlow.Domain.Common.Commands;

namespace SmartGlow.Application.Streets.Commands;

/// <summary>
/// Represents a command to delete an existing street.
/// </summary>
public record StreetDeleteByIdCommand : ICommand<bool>
{
    /// <summary>
    /// The unique identifier of the organization to be deleted.
    /// </summary>
    public Guid StreetId { get; set; } 
}