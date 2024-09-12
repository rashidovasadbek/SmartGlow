using MediatR;

namespace SmartGlow.Domain.Common.Commands;

/// <summary>
/// Defines a command handler that handles a command and produces a result.
/// </summary>
/// <typeparam name="TCommand">The type of command being handled.</typeparam>
/// <typeparam name="TResult">The type of result produced by handling the command.</typeparam>
public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, TResult>
    where TCommand : ICommand<TResult>;

/// <summary>
/// Defines a command handler that handles a command but does not produce a result.
/// </summary>
/// <typeparam name="TCommand">The type of command being handled.</typeparam>
public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand> where TCommand : ICommand;
