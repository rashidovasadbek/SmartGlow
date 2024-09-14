using System.Linq.Expressions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SmartGlow.Application.Streets.Models;
using SmartGlow.Application.Streets.Services;
using SmartGlow.Domain.Brokers;
using SmartGlow.Domain.Common.Commands;
using SmartGlow.Domain.Common.Queries;
using SmartGlow.Domain.Entities;
using SmartGlow.Domain.Enums;
using SmartGlow.Infrastructure.Streets.Validators;
using SmartGlow.Persistence.Extensions;
using SmartGlow.Persistence.Repositories.Interfaces;

namespace SmartGlow.Infrastructure.Streets.Services;

public class StreetService(
    IStreetRepository streetRepository, 
    StreetValidator streetValidator,
    IRequestUserContextProvider requestUserContextProvider)
    : IStreetService
{
    public IQueryable<Street> Get(Expression<Func<Street, bool>>? predicate = default, QueryOptions queryOptions = default)
    {
        if (requestUserContextProvider.GetUserId() == Guid.Empty)
        {
            throw new UnauthorizedAccessException();
        }

        return streetRepository.Get(predicate, queryOptions);
    }

    public IQueryable<Street> Get(StreetFilter streetFilter, QueryOptions queryOptions = default)
    {
        var streetQuery = streetRepository.Get().ApplyPagination(streetFilter);

        if (streetFilter.UserId.HasValue)
            streetQuery = streetQuery.Where(street => street.UserId == streetFilter.UserId);

        return streetQuery;
    }

    public async ValueTask<Street?> GetByIdAsync(Guid streetId, QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default)
    {
        var streetQuery = streetRepository.Get(street => street.Id == streetId, queryOptions);
        
        if (requestUserContextProvider.IsLoggedIn())
            streetQuery = streetQuery
                .Where(street => street.UserId == requestUserContextProvider.GetUserId());

        return await streetQuery.FirstOrDefaultAsync(cancellationToken);
    }

    public ValueTask<Street> CreateAsync(Street street, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        var validationResult = streetValidator.Validate(street, option =>
            option.IncludeRuleSets(EntityEvent.OnCreate.ToString()));
        
        if(!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        return streetRepository.CreateAsync(street, commandOptions, cancellationToken);
    }

    public async ValueTask<Street> UpdateAsync(
        Street street,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        var validationResult = streetValidator.Validate(street, option =>
            option.IncludeRuleSets(EntityEvent.OnUpdate.ToString()));
        
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var foundStreet = await GetByIdAsync(street.Id, cancellationToken: cancellationToken) 
            ?? throw new InvalidOperationException("street not found.");
        
        if (foundStreet.UserId != street.UserId)
            throw new InvalidOperationException("Can't change street owner");

        foundStreet.StreetName = street.StreetName;
        foundStreet.Latitude = street.Latitude;
        foundStreet.Longitude = street.Longitude;
        
        return await streetRepository.UpdateAsync(foundStreet, commandOptions, cancellationToken);
    }

    public ValueTask<Street?> DeleteAsync(Street street, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return streetRepository.DeleteAsync(street, commandOptions, cancellationToken);
    }

    public async ValueTask<Street?> DeleteByIdAsync(Guid streetId, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
       var foundStreet = await GetByIdAsync(streetId, cancellationToken: cancellationToken)
           ?? throw new InvalidOperationException("Street not found");

       return await streetRepository.DeleteByIdAsync(foundStreet.Id, commandOptions, cancellationToken);
    }
}