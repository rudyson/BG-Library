using BG.NET.Library.DataAccess.Contexts;
using BG.NET.Library.DataAccess.Entities;
using BG.NET.Library.Models.Requests;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BG.NET.Library.BusinessLogic.Validators.Requests;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator(LibraryDbContext context)
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .MustAsync(async (username, cancellationToken) => await context.Users.AnyAsync(x => x.Username == username, cancellationToken))
            .WithMessage("User with specified username is not exists");
        RuleFor(x => x.Password)
            .NotEmpty()
            .MustAsync(async (loginDto, password, cancellationToken) =>
            {
                User? user = await context.Users!.FirstOrDefaultAsync(u => u.Username == loginDto.Username, cancellationToken);
                if (user == null) return false;
                return BCrypt.Net.BCrypt.Verify(password, user.Password);
            })
            .WithMessage("Wrong password provided");
    }
}