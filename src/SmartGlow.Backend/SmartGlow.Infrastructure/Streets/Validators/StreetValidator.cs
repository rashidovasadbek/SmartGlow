using FluentValidation;
using Microsoft.Extensions.Options;
using SmartGlow.Domain.Entities;
using SmartGlow.Domain.Enums;
using SmartGlow.Infrastructure.Common.Settings;

namespace SmartGlow.Infrastructure.Streets.Validators;

/// <summary>
/// Validates the properties of a <see cref="Street"/> object.
/// </summary>
public class StreetValidator : AbstractValidator<Street>
{
    public StreetValidator(IOptions<ValidationSettings> validationSettings)
    {
        RuleSet(
            EntityEvent.OnCreate.ToString(),
            () =>
            {
                RuleFor(street => street.StreetName)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(128);

                RuleFor(street => street.Latitude)
                    .NotEmpty();
                
                RuleFor(street => street.Longitude)
                    .NotEmpty();
                
                RuleFor(street => street.UserId)
                    .NotEqual(Guid.Empty);
            });

        RuleSet(
            EntityEvent.OnUpdate.ToString(),
            () =>
            {
                RuleFor(street => street.StreetName)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(128);

                RuleFor(street => street.Latitude)
                    .NotEmpty();
                
                RuleFor(street => street.Longitude)
                    .NotEmpty();
                
                RuleFor(street => street.UserId)
                    .NotEqual(Guid.Empty);
            });
    }
}