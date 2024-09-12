using MediatR;

namespace SmartGlow.Domain.Common.Events;

public interface IEvent : INotification
{
    /// <summary>
    /// Gets or sets the unique identifier of the event.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Gets or sets the timestamp when the event was created.
    /// </summary>
    public DateTimeOffset CreatedTime { get; set; }
    
    /// <summary>
    /// Gets or sets a flag indicating whether the event has been redelivered.
    /// </summary>
    public bool Redelivered { get; set; }
}