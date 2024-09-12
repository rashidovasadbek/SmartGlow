using SmartGlow.Domain.Common.Events;
using SmartGlow.Domain.Entities;

namespace SmartGlow.Application.Common.Identity.Events;

/// <summary>
/// This class represents an event that is triggered when a user is created.
/// </summary>
public record UserCreatedEvent(User createdUser): EventBase
{
    /// <summary>
    /// Gets or sets the information about the user that was created.
    /// </summary>
    public User CreatedUser { get; set; } = createdUser;
}