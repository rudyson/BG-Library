using BGNet.TestAssignment.BusinessLogic.Interfaces.Library;
using BGNet.TestAssignment.Common.WebApi.Models.Pagination;
using BGNet.TestAssignment.DataAccess.Contexts;
using BGNet.TestAssignment.DataAccess.Entities;
using BGNet.TestAssignment.Models.Dto.Library;
using BGNet.TestAssignment.Models.Requests.Library;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace BGNet.TestAssignment.BusinessLogic.Services.Library;

public class BookService : IBookService
{
    private readonly LibraryDbContext _context;

    public BookService(LibraryDbContext context)
    {
        _context = context;
    }
    public async Task<BookShortInfoDto?> CreateAsync(BookCreateRequest book, CancellationToken cancellationToken)
    {
        var mappedBookToCreate = book.Adapt<Book>();
        if (book.AuthorId > 0)
        {
            var author = await _context.Authors!.SingleOrDefaultAsync(x => x.Id == book.AuthorId, cancellationToken: cancellationToken);
            if (author == null) return null;
            mappedBookToCreate.Author = author;
        }
        var createdBook = await _context.Books.AddAsync(mappedBookToCreate, cancellationToken: cancellationToken);
        if (createdBook.State == EntityState.Added)
        {
            await _context.SaveChangesAsync(cancellationToken: cancellationToken);
            return createdBook.Entity.Adapt<BookShortInfoDto>();
        }
        return null;
    }

    public async Task<BookFullInfoDto?> FindFullAsync(int id, CancellationToken cancellationToken)
    {
        var book = await _context.Books!.Include(b => b.Author).SingleOrDefaultAsync(b => b.Id == id, cancellationToken: cancellationToken);
        return book?.Adapt<BookFullInfoDto>();
    }

    public async Task<IEnumerable<BookFullInfoDto>?> AllFullAsync(CancellationToken cancellationToken)
    {
        var books = await _context.Books!
            .Include(b => b.Author)
            .OrderBy(x => x.Title)
            .ToListAsync(cancellationToken: cancellationToken);
        return books?.Adapt<List<BookFullInfoDto>>();
    }

    public async Task<GenericPaginationModel<BookFullInfoDto>?> AllPaginatedPageSizeFullAsync(int page, int size, CancellationToken cancellationToken)
    {
        var books = _context.Books!
            .Include(b => b.Author)
            .OrderBy(x => x.Title);
        var total = await books.CountAsync(cancellationToken: cancellationToken);
        var numberSkipped = PaginationCalculationAssistant.Skipped(page, size);
        var entities = await books
                    .Skip(numberSkipped)
                    .Take(size)
                    .ToListAsync(cancellationToken: cancellationToken);
        return new GenericPaginationModel<BookFullInfoDto>
        {
            Page = page,
            PageSize = size,
            TotalSize = total,
            Pages = PaginationCalculationAssistant.TotalPages(total, size),
            NumberSkipped = numberSkipped,
            Entities = total > 0 ? entities.Adapt<List<BookFullInfoDto>>() : new List<BookFullInfoDto>()
        };
    }

    public async Task<GenericPaginationModel<BookFullInfoDto>?> AllPaginatedSkipTakeFullAsync(int skip, int take, CancellationToken cancellationToken)
    {
        var books = _context.Books!
            .Include(b => b.Author)
            .OrderBy(x => x.Title);
        var total = await books.CountAsync(cancellationToken: cancellationToken);
        var entities = await books
                    .Skip(skip)
                    .Take(take)
                    .ToListAsync(cancellationToken: cancellationToken);
        return new GenericPaginationModel<BookFullInfoDto>
        {
            Page = PaginationCalculationAssistant.CurrentPageSkipTake(skip, take),
            PageSize = take,
            TotalSize = total,
            Pages = PaginationCalculationAssistant.TotalPages(total, take),
            NumberSkipped = skip,
            Entities = total > 0 ? entities.Adapt<List<BookFullInfoDto>>() : new List<BookFullInfoDto>()
        };
    }

    public async Task<BookShortInfoDto?> UpdateAsync(int id, BookUpdateRequest book, CancellationToken cancellationToken)
    {
        var bookInstance = await _context.Books!.Include(x => x.Author).SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (bookInstance == null) return null;
        var entryChanged = false;

        if (book.Genre != null && bookInstance.Genre != book.Genre)
        {
            bookInstance.Genre = book.Genre;
            entryChanged = true;
        }

        if (book.Title != null && bookInstance.Title != book.Title)
        {
            bookInstance.Title = book.Title;
            entryChanged = true;
        }

        if (book.PublishYear.HasValue && bookInstance.PublishYear != book.PublishYear)
        {
            bookInstance.PublishYear = book.PublishYear.Value;
            entryChanged = true;
        }

        if (book.AuthorId != null)
        {
            var author = await _context.Authors.SingleOrDefaultAsync(x => x.Id == book.AuthorId, cancellationToken);
            if (author != null && bookInstance.Author!.Id != book.AuthorId)
            {
                bookInstance.Author = author;
                entryChanged = true;
            }
        }
        if (entryChanged && await _context.SaveChangesAsync(cancellationToken: cancellationToken) > 0)
            return bookInstance.Adapt<BookShortInfoDto>();
        return null;
    }

    public async Task<BookFullInfoDto?> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var bookInstance = await _context.Books!.SingleOrDefaultAsync(b => b.Id == id, cancellationToken: cancellationToken);
        if (bookInstance == null) return null;
        _context.Books!.Remove(bookInstance);
        if (await _context.SaveChangesAsync(cancellationToken: cancellationToken) > 0)
            return bookInstance.Adapt<BookFullInfoDto?>();
        return null;
    }
    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken) => await _context.Books!.SingleOrDefaultAsync(a => a.Id == id, cancellationToken: cancellationToken) != null;
}