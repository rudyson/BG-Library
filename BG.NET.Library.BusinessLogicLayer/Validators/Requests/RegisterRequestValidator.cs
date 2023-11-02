using BG.NET.Library.DataAccess.Contexts;
using BG.NET.Library.Models.Requests;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BG.NET.Library.BusinessLogic.Validators.Requests;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator(LibraryDbContext context)
    {
        RuleFor(x => x.Username)
            .MustAsync(async (username, cancellationToken) => !await context.Users.AnyAsync(x => x.Username == username, cancellationToken))
            .WithMessage("This username is already taken");
        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8).WithMessage("Minimal length of password is 8 characters")
            .Matches(@"[A-Z]+").WithMessage("Use uppercase letters in password")
            .Matches(@"[a-z]+").WithMessage("Use lowercase letters in password")
            .Matches(@"[0-9]+").WithMessage("Use at least one number in password");
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Surname).NotEmpty();
        RuleFor(x => x.Birthday).NotEmpty();
        RuleFor(x => x.Address).NotEmpty();
    }
}