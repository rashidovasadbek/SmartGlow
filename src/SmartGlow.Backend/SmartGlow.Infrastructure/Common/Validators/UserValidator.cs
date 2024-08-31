using FluentValidation;
using Microsoft.Extensions.Options;
using SmartGlow.Domain.Entities;
using SmartGlow.Domain.Enums;
using SmartGlow.Infrastructure.Settings;

namespace SmartGlow.Infrastructure.Common.Validators;

public class UserValidator : AbstractValidator<User> 
{
    public UserValidator(IOptions<ValidationSettings> validationSettings)
    {
        var settings = validationSettings.Value;
        
        RuleSet(
            IdentityEvent.OnSignUpUser.ToString(),
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
                    .MinimumLength(13)
                    .MaximumLength(16)
                    .Matches(settings.PhoneNumberRegexPattern);
                
                RuleFor(user => user.Password)
                    .NotEmpty()
                    .MinimumLength(8)
                    .Matches(settings.PasswordRegexPattern);
            });
    }
}