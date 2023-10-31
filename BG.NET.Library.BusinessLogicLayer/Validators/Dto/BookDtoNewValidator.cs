using BG.NET.Library.Models.Dto.Library;
using FluentValidation;

namespace BG.NET.Library.BusinessLogicLayer.Validators.Dto
{
    public class BookDtoNewValidator : AbstractValidator<BookDtoNew>
    {
        public BookDtoNewValidator() {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Genre).NotEmpty();
            RuleFor(x => x.PublishYear).NotEmpty().GreaterThan(0);
            RuleFor(x => x.AuthorId).GreaterThanOrEqualTo(1).When(x => x.AuthorId!=null);
        }
    }
}
