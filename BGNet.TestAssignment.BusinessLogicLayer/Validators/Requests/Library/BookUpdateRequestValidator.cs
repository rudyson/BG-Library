using BGNet.TestAssignment.DataAccess.Contexts;
using BGNet.TestAssignment.Models.Requests.Library;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BGNet.TestAssignment.BusinessLogic.Validators.Requests.Library
{
    public class BookUpdateRequestValidator : AbstractValidator<BookUpdateRequest>
    {
        public BookUpdateRequestValidator(LibraryDbContext context)
        {
            /*
            RuleFor(x => x.PublishYear)
                .GreaterThan(0)
                .When(x=>x.PublishYear!=null);
            RuleFor(x => x.AuthorId)
                .GreaterThanOrEqualTo(1).When(x => x.AuthorId != null)
                .MustAsync(async (authorId, cancellationToken) => await context.Authors.AnyAsync(x => x.Id == authorId, cancellationToken))
                .When(x => x.AuthorId != null)
                .WithMessage(x => $"Author with id {x.AuthorId} is not exists");
            */
        }
    }
}
