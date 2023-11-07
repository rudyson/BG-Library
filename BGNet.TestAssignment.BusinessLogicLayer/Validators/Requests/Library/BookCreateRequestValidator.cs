using BGNet.TestAssignment.DataAccess.Contexts;
using BGNet.TestAssignment.Models.Requests.Library;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BGNet.TestAssignment.BusinessLogic.Validators.Requests.Library
{
    public class BookCreateRequestValidator : AbstractValidator<BookCreateRequest>
    {
        public BookCreateRequestValidator(LibraryDbContext context)
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Genre).NotEmpty();
            RuleFor(x => x.PublishYear).NotEmpty().GreaterThan(0);
            RuleFor(x => x.AuthorId)
                .NotEmpty()
                .GreaterThanOrEqualTo(1)
                .MustAsync(async (authorId, cancellationToken) => await context.Authors.AnyAsync(x => x.Id == authorId, cancellationToken))
                .WithMessage(id => $"Author with id {id} is not exists");
        }
    }
}
