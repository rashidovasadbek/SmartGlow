using System.Linq.Expressions;
using SmartGlow.Application.Streets.Models;
using SmartGlow.Domain.Common.Commands;
using SmartGlow.Domain.Common.Queries;
using SmartGlow.Domain.Entities;

namespace SmartGlow.Application.Streets.Services;

/// <summary>
/// Defines a contract for services that interact with street data.
/// </summary>
public interface IStreetService
{
     /// <summary>
    /// Retrieves a queryable collection of Streets, optionally filtered by a predicate.
    /// </summary>
    /// <param name="predicate">An optional expression to filter the retrieved Streets.</param>
    /// <param name="queryOptions">If true, disables change tracking for the returned entities, potentially improving performance.</param>
    /// <returns>An IQueryable&lt;Street&gt; that allows for further LINQ-based querying and filtering.</returns>
    IQueryable<Street> Get(Expression<Func<Street, bool>>? predicate = default, QueryOptions queryOptions = default);

    /// <summary>
    /// Retrieves a filtered and potentially paginated queryable collection of organizations.
    /// </summary>
    /// <param name="streetFilter">The criteria used to filter the results.</param>
    /// <param name="queryOptions">Optional parameters to configure pagination, sorting, etc. (default is no modification).</param>
    /// <returns>An IQueryable of <see cref="Street"/> instances matching the specified filter and query options.</returns>
    IQueryable<Street> Get(StreetFilter streetFilter, QueryOptions queryOptions = default);

    /// <summary>
    /// Retrieves an Organization by its unique identifier (ID) asynchronously.
    /// </summary>
    /// <param name="streetId">The unique ID of the Organization.</param>
    /// <param name="queryOptions">If true, disables change tracking for the returned entity, potentially improving performance.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A ValueTask&lt;Organization?&gt; representing the asynchronous operation. The result will be the requested Organization if found, otherwise null.</returns>
    ValueTask<Street?> GetByIdAsync(Guid streetId, QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Creates a new street record asynchronously.
    /// </summary>
    /// <param name="street">The Street object to be created.</param>
    /// <param name="commandOptions">If true, automatically saves changes to the underlying data store. Otherwise, allows for more changes before saving.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>The newly created Street object.</returns>
    /// <exception cref="ArgumentException">Thrown if the provided 'street' has a default (empty) ID or if an street with the same ID already exists.</exception>
    public ValueTask<Street> CreateAsync(
        Street street,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Updates an existing Street record asynchronously.
    /// </summary>
    /// <param name="street">The Street object containing the modified data.</param>
    /// <param name="commandOptions">Controls whether changes are immediately persisted to the data store.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>The updated Street object.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the provided 'Street' is null.</exception>

    ValueTask<Street> UpdateAsync(
        Street street,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Deletes an existing Street record asynchronously, based on the provided Organization object.
    /// </summary>
    /// <param name="street">The Street object to be deleted.</param>
    /// <param name="commandOptions">Controls whether changes are immediately persisted to the data store.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>The deleted Street object if successful, otherwise null (e.g., if the Organization was not found).</returns>
    ValueTask<Street?> DeleteAsync(
        Street street,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Deletes an existing Street record asynchronously, based on its unique identifier (ID).
    /// </summary>
    /// <param name="streetId">The ID of the Organization to be deleted.</param>
    /// <param name="commandOptions">Controls whether changes are immediately persisted to the data store.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>The deleted Organization object if successful, otherwise null (e.g., if the Organization was not found).</returns>
    ValueTask<Street?> DeleteByIdAsync(
        Guid streetId,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    );
}