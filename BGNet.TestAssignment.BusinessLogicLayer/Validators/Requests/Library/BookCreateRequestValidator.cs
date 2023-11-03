using BGNet.TestAssignment.Models.Requests.Library;
using FluentValidation;

namespace BGNet.TestAssignment.BusinessLogic.Validators.Requests.Library
{
    public class BookCreateRequestValidator : AbstractValidator<BookCreateRequest>
    {
        public BookCreateRequestValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Genre).NotEmpty();
            RuleFor(x => x.PublishYear).NotEmpty().GreaterThan(0);
            RuleFor(x => x.AuthorId).GreaterThanOrEqualTo(1).When(x => x.AuthorId != null);
        }
    }
}
