using BGNet.TestAssignment.Models.Requests.Library;
using FluentValidation;

namespace BGNet.TestAssignment.BusinessLogic.Validators.Requests.Library
{
    public class AuthorUpdateRequestValidator : AbstractValidator<AuthorUpdateRequest>
    {
        public AuthorUpdateRequestValidator()
        {
            RuleFor(x => x.Birthday).GreaterThan(DateOnly.MinValue);
        }
    }
}
