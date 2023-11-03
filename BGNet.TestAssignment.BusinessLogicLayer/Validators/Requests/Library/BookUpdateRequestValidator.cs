using BGNet.TestAssignment.Models.Requests.Library;
using FluentValidation;

namespace BGNet.TestAssignment.BusinessLogic.Validators.Requests.Library
{
    public class BookUpdateRequestValidator : AbstractValidator<BookUpdateRequest>
    {
        public BookUpdateRequestValidator()
        {
            RuleFor(x => x.PublishYear).GreaterThan(0);
            RuleFor(x => x.AuthorId).GreaterThanOrEqualTo(1).When(x => x.AuthorId != null);
        }
    }
}
