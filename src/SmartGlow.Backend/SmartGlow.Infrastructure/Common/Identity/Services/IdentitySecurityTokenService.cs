using FluentValidation;
using SmartGlow.Application.Common.Identity.Services;
using SmartGlow.Domain.Common.Commands;
using SmartGlow.Domain.Entities;
using SmartGlow.Domain.Enums;
using SmartGlow.Persistence.Repositories.Interfaces;

namespace SmartGlow.Infrastructure.Common.Identity.Services;

public class IdentitySecurityTokenService(
    IAccessTokenRepository accessTokenRepository,
    IRefreshTokenRepository refreshTokenRepository,
    IValidator<RefreshToken> refreshTokenValidator,
    IValidator<AccessToken> accessTokenValidator) : IIdentitySecurityTokenService
{
    public ValueTask<AccessToken> CreateAccessTokenAsync(AccessToken accessToken, CommandOptions commandOptions,
        CancellationToken cancellationToken = default)
    {
        var validationResult = accessTokenValidator
            .Validate(accessToken,
                options => options.IncludeRuleSets(EntityEvent.OnCreate.ToString()));
        
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        return accessTokenRepository.CreateAsync(accessToken, commandOptions, cancellationToken);

    }

    public ValueTask<AccessToken?> GetAccessTokenByIdAsync(Guid accessTokenId, CancellationToken cancellationToken = default)
    {
        return accessTokenRepository.GetByIdAsync(accessTokenId, cancellationToken);
    }

    public ValueTask<RefreshToken> CreateRefreshTokenAsync(
        RefreshToken refreshToken, 
        CommandOptions commandOptions, 
        CancellationToken cancellationToken = default)
    {
        var validationResult = refreshTokenValidator.Validate(refreshToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        return refreshTokenRepository.CreateAsync(refreshToken, cancellationToken);
    }

    public ValueTask<RefreshToken?> GetRefreshTokenByValueAsync(string refreshTokenValue, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async ValueTask RevokeAccessTokenAsync(Guid accessTokenId, CancellationToken cancellationToken = default)
    {
        await accessTokenRepository.DeleteByIdAsync(accessTokenId, cancellationToken);
    }

    public async ValueTask RemoveAccessTokenAsync(Guid accessTokenId, CancellationToken cancellationToken = default)
    {
        await accessTokenRepository.DeleteByIdAsync(accessTokenId, cancellationToken);
    }

    public ValueTask RemoveRefreshTokenAsync(string refreshTokenValue, CancellationToken cancellationToken = default)
    {
        return refreshTokenRepository.RemoveAsync(refreshTokenValue, cancellationToken);
    }
}