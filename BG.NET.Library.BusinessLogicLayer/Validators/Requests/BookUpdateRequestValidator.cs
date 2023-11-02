using BG.NET.Library.Models.Requests;
using FluentValidation;

namespace BG.NET.Library.BusinessLogic.Validators.Requests
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
