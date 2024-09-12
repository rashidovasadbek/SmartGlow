using Microsoft.Extensions.DependencyInjection;
using SmartGlow.Application.Common.Identity.Events;
using SmartGlow.Domain.Common.Events;

namespace SmartGlow.Infrastructure.Common.Identity.EventHandlers;

public class UserCreatedEventHandler(IServiceScopeFactory serviceScopeFactory) : EventHandlerBase<UserCreatedEvent>
{
    protected override ValueTask HandleAsync(UserCreatedEvent @event, CancellationToken cancellationToken)
    {
        return ValueTask.CompletedTask;
    }
}