using BG.NET.Library.Models.Dto.Library;
using FluentValidation;

namespace BG.NET.Library.BusinessLogicLayer.Validators.Dto
{
    public class BookDtoUpdateValidator : AbstractValidator<BookDtoUpdate>
    {
        public BookDtoUpdateValidator() {
            RuleFor(x => x.PublishYear).GreaterThan(0);
            RuleFor(x => x.AuthorId).GreaterThanOrEqualTo(1).When(x => x.AuthorId != null);
        }
    }
}
