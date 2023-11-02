using BG.NET.Library.Models.Requests;
using FluentValidation;

namespace BG.NET.Library.BusinessLogic.Validators.Requests
{
    public class AuthorDtoNewValidator : AbstractValidator<AuthorCreateRequest>
    {
        public AuthorDtoNewValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Surname).NotEmpty();
            RuleFor(x => x.Birthday).NotEmpty().GreaterThan(DateOnly.MinValue);
        }
    }
}
