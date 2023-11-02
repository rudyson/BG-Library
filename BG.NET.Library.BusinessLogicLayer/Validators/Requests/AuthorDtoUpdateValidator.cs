using BG.NET.Library.Models.Requests;
using FluentValidation;

namespace BG.NET.Library.BusinessLogic.Validators.Requests
{
    public class AuthorDtoUpdateValidator : AbstractValidator<AuthorUpdateRequest>
    {
        public AuthorDtoUpdateValidator()
        {
            RuleFor(x => x.Birthday).GreaterThan(DateOnly.MinValue);
        }
    }
}
