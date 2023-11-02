using BG.NET.Library.Models.Requests;
using FluentValidation;

namespace BG.NET.Library.BusinessLogic.Validators.Requests
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
