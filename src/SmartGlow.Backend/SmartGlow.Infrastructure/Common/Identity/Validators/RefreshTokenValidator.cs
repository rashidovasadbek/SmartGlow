using FluentValidation;
using Microsoft.Extensions.Options;
using SmartGlow.Domain.Entities;
using SmartGlow.Domain.Enums;
using SmartGlow.Infrastructure.Common.Settings;

namespace SmartGlow.Infrastructure.Common.Identity.Validators;

public class RefreshTokenValidator : AbstractValidator<RefreshToken>
{
    public RefreshTokenValidator(IOptions<JwtSettings> jwtSettings)
    {
        var identityTokenSettingsValue = jwtSettings.Value;

        RuleSet(
            EntityEvent.OnCreate.ToString(),
            () =>
            {
                RuleFor(refreshToken => refreshToken.Id).NotEqual(Guid.Empty);

                RuleFor(refreshToken => refreshToken.Token).NotEmpty();

                RuleFor(refreshToken => refreshToken.UserId).NotEqual(Guid.Empty);

                RuleFor(refreshToken => refreshToken.ExpiryTime)
                    .GreaterThan(DateTimeOffset.UtcNow)
                    .Custom(
                        (refreshToken, context) =>
                        {
                            if (refreshToken >
                                DateTimeOffset.UtcNow.AddMinutes(identityTokenSettingsValue.RefreshTokenExtendedExpirationTimeInMinutes))
                                context.AddFailure(
                                    nameof(RefreshToken.ExpiryTime),
                                    $"{nameof(RefreshToken.ExpiryTime)} cannot be greater than the expiration time of the JWT token."
                                );
                        }
                    )
                    .When(refreshToken => refreshToken.EnableExtendedExpiryTime)
                    .Custom(
                        (refreshToken, context) =>
                        {
                            if (refreshToken > DateTimeOffset.UtcNow.AddMinutes(identityTokenSettingsValue.RefreshTokenExpirationTimeInMinutes))
                                context.AddFailure(
                                    nameof(RefreshToken.ExpiryTime),
                                    $"{nameof(RefreshToken.ExpiryTime)} cannot be greater than the expiration time of the JWT token."
                                );
                        }
                    )
                    .When(refreshToken => !refreshToken.EnableExtendedExpiryTime);
            }
        );
    }
}