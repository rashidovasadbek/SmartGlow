using FluentValidation;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;
using SmartGlow.Domain.Entities;
using SmartGlow.Domain.Enums;
using SmartGlow.Infrastructure.Common.Settings;

namespace SmartGlow.Infrastructure.Users.Validators;

public class UserValidator : AbstractValidator<User> 
{
    public UserValidator(IOptions<ValidationSettings> validationSettings)
    {
        var settings = validationSettings.Value;
        
        RuleSet(
            EntityEvent.OnCreate.ToString(),
            () =>
            {
                RuleFor(user => user.FirstName)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(128)
                    .Matches(settings.PersonNameRegexPattern);
                
                RuleFor(user => user.LastName)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(128)
                    .Matches(settings.PersonNameRegexPattern);
                
                RuleFor(user => user.UserName)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(128)
                    .Matches(settings.PersonNameRegexPattern);
                
                RuleFor(user => user.PhoneNumber)
                    .NotEmpty()
                    .MinimumLength(10)
                    .MaximumLength(15)
                    .Matches(settings.PhoneNumberRegexPattern);

                RuleFor(user => user.PasswordHash).NotEmpty();
            });
        
    }
}