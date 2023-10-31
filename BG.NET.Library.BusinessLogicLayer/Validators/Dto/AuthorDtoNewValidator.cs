using BG.NET.Library.Models.Dto.Library;
using FluentValidation;

namespace BG.NET.Library.BusinessLogicLayer.Validators.Dto
{
    public class AuthorDtoNewValidator : AbstractValidator<AuthorDtoBase>
    {
        public AuthorDtoNewValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Surname).NotEmpty();
            RuleFor(x => x.Birthday).NotEmpty().GreaterThan(DateOnly.MinValue);
        }
    }
}
