using MediatR;

namespace SmartGlow.Domain.Common.Queries;

/// <summary>
/// Defines a query handler that handles a query and produces a result.
/// </summary>
/// <typeparam name="TQuery">The type of query being handled.</typeparam>
/// <typeparam name="TResult">The type of result produced by handling the query.</typeparam>
public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult>;