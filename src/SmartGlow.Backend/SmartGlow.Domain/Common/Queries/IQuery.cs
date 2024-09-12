using MediatR;

namespace SmartGlow.Domain.Common.Queries;

// <summary>
/// Defines a query that returns a result
/// </summary>
/// <typeparam name="TResult">The type of result returned by the query.</typeparam>
public interface IQuery<out TResult> : IRequest<TResult>, IQuery;

/// <summary>
/// Defines a query
/// </summary>
public interface IQuery;