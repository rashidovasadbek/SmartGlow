using SmartGlow.Domain.Common.Queries;
using SmartGlow.Domain.Entities;

namespace SmartGlow.Application.Common.Identity.Services;

/// <summary>
/// Represents a service for managing user accounts.
/// </summary>
public interface IAccountService
{
    /// <summary>
    /// Retrieves a user by their UserName
    /// </summary>
    /// <param name="username">The email address of the user to retrieve</param>
    /// <param name="queryOptions">Indicates whether to disable change tracking. Default is false.</param>
    /// <param name="cancellationToken">Cancellation token for asynchronous operations.</param>
    /// <returns>containing the retrieved user or null if not found.</returns>
    ValueTask<User> GetUserByUsernameAsync(string username, QueryOptions queryOptions, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="user">The user object to be created.</param>
    /// <param name="cancellationToken">Cancellation token for asynchronous operations.</param>
    /// <returns>True if the user is created successfully; otherwise, false.</returns>
    ValueTask<User> CreateUserAsync(User user, CancellationToken cancellationToken = default);
}