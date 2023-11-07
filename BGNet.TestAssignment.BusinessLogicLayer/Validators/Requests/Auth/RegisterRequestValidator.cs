using BGNet.TestAssignment.DataAccess.Contexts;
using BGNet.TestAssignment.Models.Requests.Auth;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BGNet.TestAssignment.BusinessLogic.Validators.Requests.Auth;

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
            .MaximumLength(32).WithMessage("Maximum length of password is 32 characters");
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Surname).NotEmpty();
        RuleFor(x => x.Birthday).NotEmpty();
        RuleFor(x => x.Address).NotEmpty();
    }
}