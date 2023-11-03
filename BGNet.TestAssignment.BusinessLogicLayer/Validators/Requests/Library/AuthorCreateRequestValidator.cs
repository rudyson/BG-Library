using BGNet.TestAssignment.Models.Requests.Library;
using FluentValidation;

namespace BGNet.TestAssignment.BusinessLogic.Validators.Requests.Library
{
    public class AuthorCreateRequestValidator : AbstractValidator<AuthorCreateRequest>
    {
        public AuthorCreateRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Surname).NotEmpty();
            RuleFor(x => x.Birthday).NotEmpty().GreaterThan(DateOnly.MinValue);
        }
    }
}
