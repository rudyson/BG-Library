using BG.NET.Library.Models.Dto.Auth;
using FluentValidation;

namespace BG.NET.Library.BusinessLogicLayer.Validators;

public class LoginUserValidator : AbstractValidator<LoginDto>
{
    public LoginUserValidator()
    {
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
    }
}