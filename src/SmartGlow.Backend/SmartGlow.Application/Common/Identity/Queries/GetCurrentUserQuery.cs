using SmartGlow.Domain.Common.Queries;
using SmartGlow.Domain.Entities;

namespace SmartGlow.Application.Common.Identity.Queries;

/// <summary>
/// Represents get current user query
/// </summary>
public class GetCurrentUserQuery : IQuery<User>;