using BGNet.TestAssignment.DataAccess.Contexts;
using BGNet.TestAssignment.DataAccess.Entities;
using BGNet.TestAssignment.Common.WebApi.Models.Pagination;
using Mapster;
using Microsoft.EntityFrameworkCore;
using BGNet.TestAssignment.BusinessLogic.Interfaces.Library;
using BGNet.TestAssignment.Models.Requests.Library;
using BGNet.TestAssignment.Models.Dto.Library;

namespace BGNet.TestAssignment.BusinessLogic.Services.Library;

public class BookService : IBookService
{
    private readonly LibraryDbContext _context;

    public BookService(LibraryDbContext context)
    {
        _context = context;
    }
    public async Task<BookShortInfoDto?> Create(BookCreateRequest book)
    {
        var mappedBookToCreate = book.Adapt<Book>();
        if (book.AuthorId != null)
        {
            var author = await _context.Authors!.FindAsync(book.AuthorId);
            if (author != null)
                mappedBookToCreate.Author = author;
        }
        var createdBook = await _context.Books!.AddAsync(mappedBookToCreate);
        return
            await _context.SaveChangesAsync() > 0
                ? createdBook.Entity.Adapt<BookShortInfoDto>()
                : null;
    }

    public async Task<BookFullInfoDto?> FindFull(int id)
    {
        var book = await _context.Books!.Include(b => b.Author).SingleOrDefaultAsync(b => b.Id == id);
        return book?.Adapt<BookFullInfoDto>();
    }

    public async Task<IEnumerable<BookFullInfoDto>?> AllFull()
    {
        var books = await _context.Books!
            .Include(b => b.Author)
            .OrderBy(x => x.Title)
            .ToListAsync();
        return books?.Adapt<List<BookFullInfoDto>>();
    }

    public async Task<GenericPaginationModel<BookFullInfoDto>?> AllPaginatedFull(int page, int size)
    {
        var books = _context.Books!
            .Include(b => b.Author)
            .OrderBy(x => x.Title);
        var total = await books.CountAsync();
        var numberSkipped = PaginationCalculationAssistant.Skipped(page,size);
        var entities = await books
                    .Skip(numberSkipped)
                    .Take(size)
                    .ToListAsync();
        return new GenericPaginationModel<BookFullInfoDto>
        {
            Page = page,
            PageSize = size,
            TotalSize = total,
            Pages = PaginationCalculationAssistant.TotalPages(total,size),
            NumberSkipped = numberSkipped,
            Entities = total > 0 ? entities.Adapt<List<BookFullInfoDto>>() : new List<BookFullInfoDto>()
        };
    }

    public async Task<BookShortInfoDto?> Update(int id, BookUpdateRequest book)
    {
        var bookInstance = await _context.Books!.FindAsync(id);
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
            var author = await _context.Authors.FindAsync(book.AuthorId);
            if (author != null)
            {
                if (book.AuthorId == 0)
                {
                    bookInstance.Author = null;
                    entryChanged = true;
                }
                else
                {
                    if (bookInstance.Author!.Id != book.AuthorId)
                    {
                        bookInstance.Author = author;
                        entryChanged = true;
                    }
                }
            }
        }
        if (entryChanged && await _context.SaveChangesAsync() > 0)
            return bookInstance.Adapt<BookShortInfoDto>();
        return null;
    }

    public async Task<bool> Delete(int id)
    {
        var bookInstance = await _context.Books!.SingleOrDefaultAsync(b => b.Id == id);
        if (bookInstance == null) return false;
        _context.Books!.Remove(bookInstance);
        return await _context.SaveChangesAsync() > 0;
    }
    public async Task<bool> Exists(int id) => await _context.Books!.SingleOrDefaultAsync(a => a.Id == id) != null;
}