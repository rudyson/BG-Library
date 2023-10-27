using BG.NET.Library.Models.Dto.Auth;
using FluentValidation;

namespace BG.NET.Library.BusinessLogicLayer.Validators;

public class RegisterUserValidator : AbstractValidator<RegisterDto>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Surname).NotEmpty();
        RuleFor(x => x.Birthday).NotEmpty();
        RuleFor(x => x.Address).NotEmpty();
    }
}