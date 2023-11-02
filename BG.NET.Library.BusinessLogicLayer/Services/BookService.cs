using BG.NET.Library.BusinessLogic.Interfaces;
using BG.NET.Library.DataAccess.Contexts;
using BG.NET.Library.DataAccess.Entities;
using BG.NET.Library.Models.Dto;
using BG.NET.Library.Models.Generic;
using BG.NET.Library.Models.Requests;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace BG.NET.Library.BusinessLogic.Services;

public class BookService : IBookService
{
    private readonly LibraryDbContext _context;

    public BookService(LibraryDbContext context)
    {
        _context = context;
    }
    public async Task<BookFullInfoDto?> Create(BookCreateRequest book)
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
                ? createdBook.Entity.Adapt<BookFullInfoDto>()
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
        var numberSkipped = (page - 1) * size;
        var entities = await books
                    .Skip(numberSkipped)
                    .Take(size)
                    .ToListAsync();
        return new GenericPaginationModel<BookFullInfoDto>
        {
            Page = page,
            PageSize = size,
            TotalSize = total,
            Pages = ((total - 1) / size) + 1, //(int)Math.Ceiling((decimal)countAll / size),
            NumberSkipped = numberSkipped,
            Entities = entities.Adapt<List<BookFullInfoDto>>()
        };
    }

    public async Task<BookFullInfoDto?> Update(int id, BookUpdateRequest book)
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
            if (!(bookInstance.Author != null && bookInstance.Author.Id == book.AuthorId))
            {
                var author = await _context.Authors!.FindAsync(book.AuthorId);
                if (author != null)
                {
                    bookInstance.Author = author;
                    entryChanged = true;
                }
            }
        }
        else
        {
            if (bookInstance.Author != null)
            {
                bookInstance.Author = null;
                entryChanged = true;
            }
        }
        if (entryChanged && await _context.SaveChangesAsync() > 0)
            return bookInstance.Adapt<BookFullInfoDto>();
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