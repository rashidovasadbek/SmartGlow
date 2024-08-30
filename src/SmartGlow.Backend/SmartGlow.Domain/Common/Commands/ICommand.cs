using MediatR;

namespace SmartGlow.Domain.Common.Commands;

/// <summary>
/// Defines a command that produces a result when executed.
/// </summary>
/// <typeparam name="TResult">The type of result produced by executing the command.</typeparam>
public interface ICommand<out TResult> : IRequest<TResult>;

/// <summary>
/// Defines a command that does not produce a result when executed.
/// </summary>
public interface ICommand : IRequest;