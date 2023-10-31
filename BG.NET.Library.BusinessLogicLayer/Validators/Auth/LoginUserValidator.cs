using BG.NET.Library.Models.Dto.Auth;
using FluentValidation;

namespace BG.NET.Library.BusinessLogicLayer.Validators.Auth;

public class LoginUserValidator : AbstractValidator<LoginDto>
{
    public LoginUserValidator()
    {
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8).WithMessage("Minimal length of password is 8 characters")
            .Matches(@"[A-Z]+").WithMessage("Use uppercase letters in password")
            .Matches(@"[a-z]+").WithMessage("Use lowercase letters in password")
            .Matches(@"[0-9]+").WithMessage("Use at least one number in password");
    }
}