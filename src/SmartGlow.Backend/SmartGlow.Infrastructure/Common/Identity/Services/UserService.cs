using System.Linq.Expressions;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SmartGlow.Application.Common.Identity;
using SmartGlow.Domain.Common.Commands;
using SmartGlow.Domain.Common.Queries;
using SmartGlow.Domain.Entities;
using SmartGlow.Domain.Enums;
using SmartGlow.Domain.Extensions;
using SmartGlow.Persistence.Caching.Brokers;
using SmartGlow.Persistence.Repositories.Interfaces;

namespace SmartGlow.Infrastructure.Common.Identity.Services;

/// <summary>
/// Provides user foundation service functionality
/// </summary>
public class UserService(IMapper mapper, ICacheBroker cacheBroker, IValidator<User> userValidator, IUserRepository userRepository) : IUserService
{
    public IQueryable<User> Get(Expression<Func<User, bool>>? predicate = default, QueryOptions queryOptions = default)
    {
        return userRepository.Get(predicate, queryOptions);
    }

    public ValueTask<User?> GetByIdAsync(Guid userId, QueryOptions queryOptions = default, CancellationToken cancellationToken = default)
    {
        return userRepository.GetByIdAsync(userId, queryOptions, cancellationToken);
    }

    public async ValueTask<string?> GetPhoneNumberByIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var foundPhoneNumber = await cacheBroker.GetOrSetAsync(CacheKeyExtensions.GetPhoneNumberBy(userId), async () =>
        {
            return await Get(user => user.Id == userId).Select(user => user.PhoneNumber)
                .FirstOrDefaultAsync(cancellationToken);
        }, cancellationToken: cancellationToken);
        
        return foundPhoneNumber;
    }

    public async ValueTask<Guid?> GetIdByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken = default)
    {
        var founduserId = await cacheBroker.GetOrSetAsync(CacheKeyExtensions.GetIdByPhoneNumber(phoneNumber),
            async () =>
            {
                return await Get(user => user.PhoneNumber == phoneNumber).Select(user => (Guid?)user.Id)
                    .FirstOrDefaultAsync(cancellationToken);
            },cancellationToken: cancellationToken);
        return founduserId;
    }

    public ValueTask<bool> CheckByIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var userExistsCheck = cacheBroker.GetOrSetAsync(CacheKeyExtensions.CheckById<User>(userId), async () =>
        {
            return await userRepository.CheckAsync(Get(user => user.Id == userId)
                .Select(user => (Guid?)user.Id), cancellationToken: cancellationToken);
        }, cancellationToken: cancellationToken);
        
        return userExistsCheck;
    }

    public ValueTask<bool> CheckByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken = default)
    {
        var userExistsCheck = cacheBroker.GetOrSetAsync(CacheKeyExtensions.CheckByPhoneNumber(phoneNumber), async () =>
        {
            return await userRepository.CheckAsync(Get(user => user.PhoneNumber == phoneNumber)
                .Select(user => (Guid?)user.Id), cancellationToken: cancellationToken);
        }, cancellationToken: cancellationToken);

        return userExistsCheck;
    }

    public ValueTask<Guid> GetSystemUserIdAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<User> CreateAsync(User user, RoleType roleType, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await userValidator.ValidateAsync(
            user,
            options => options.IncludeRuleSets(mapper.Map<IdentityEvent>(roleType).ToString())
            , cancellationToken);
        
        if(!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        return await userRepository.CreateAsync(user,commandOptions ,cancellationToken);
    }

    public ValueTask<User> UpdateAsync(User user, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return userRepository.UpdateAsync(user,commandOptions,cancellationToken);
    }

    public ValueTask<User?> DeleteByIdAsync(Guid userId, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return userRepository.DeleteByIdAsync(userId,commandOptions, cancellationToken);
    }

    public ValueTask<User?> DeleteAsync(User user, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return userRepository.DeleteAsync(user, commandOptions, cancellationToken);
    }
}