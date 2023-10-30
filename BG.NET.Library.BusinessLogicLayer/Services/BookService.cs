using AutoMapper;
using BG.NET.Library.BusinessLogicLayer.Interfaces;
using BG.NET.Library.DataAccessLayer.Contexts;
using BG.NET.Library.DataAccessLayer.Interfaces;
using BG.NET.Library.Models;
using BG.NET.Library.Models.Dto.Library;
using BG.NET.Library.Models.Entities.Library;
using Microsoft.EntityFrameworkCore;

namespace BG.NET.Library.BusinessLogicLayer.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _repository;
    private readonly IMapper _mapper;
    private readonly LibraryDbContext _context;

    public BookService(IBookRepository repository, IMapper mapper, LibraryDbContext context)
    {
        _repository = repository;
        _mapper = mapper;
        _context = context;
    }
    public async Task<BookDtoFull?> Create(BookDtoNew book)
    {
        var mappedBookToCreate = _mapper.Map<BookDtoNew, Book>(book);
        if (book.AuthorId != null)
        {
            var author = await _context.Authors!.FindAsync(book.AuthorId);
            if (author!=null)
                mappedBookToCreate.Author = author;
        }
        var createdBook = await _context.Books!.AddAsync(mappedBookToCreate);
        return
            await _context.SaveChangesAsync() > 0
                ? _mapper.Map<Book, BookDtoFull>(createdBook.Entity)
                : null;
    }

    public async Task<BookDtoShort?> FindShort(int id)
    {
        var book = await _context.Books!.Include(b => b.Author).SingleOrDefaultAsync(b => b.Id == id);
        return book == null
            ? null
            : _mapper.Map<Book, BookDtoShort>(source:book);
    }

    public async Task<BookDtoFull?> FindFull(int id)
    {
        var book = await _context.Books!.Include(b => b.Author).SingleOrDefaultAsync(b => b.Id == id);
        return book == null
            ? null
            : _mapper.Map<Book, BookDtoFull>(source:book);
    }

    public async Task<IEnumerable<BookDtoShort>?> AllShort()
    {
        var books = await _context.Books!
            .Include(b => b.Author)
            .OrderBy(x => x.Title)
            .ToListAsync();
        return _mapper.Map<List<Book>, List<BookDtoShort>>(source:books);
    }

    public async Task<IEnumerable<BookDtoFull>?> AllFull()
    {
        var books = await _context.Books!
            .Include(b => b.Author)
            .OrderBy(x => x.Title)
            .ToListAsync();
        return _mapper.Map<List<Book>, List<BookDtoFull>>(source:books);
    }

    public async Task<GenericPaginationModel<BookDtoFull>?> AllPaginatedFull(int page, int size)
    {
        var books = _context.Books!
            .Include(b => b.Author)
            .OrderBy(x => x.Title);
        var countAll = await books.CountAsync();
        var numberSkipped = (page - 1) * size;
        return new GenericPaginationModel<BookDtoFull>
        {
            Page = page,
            PageSize = size,
            TotalSize = countAll,
            Pages = (int)Math.Ceiling((decimal)countAll / size),
            NumberSkipped = numberSkipped,
            Entities = _mapper.Map<List<Book>, List<BookDtoFull>>(
                source: await books
                    .Skip(numberSkipped)
                    .Take(size)
                    .ToListAsync()
                )
        };
    }

    public async Task<BookDtoShort?> Update(int id, BookDtoUpdate book)
    {
        var bookInstance = await _context.Books!.SingleOrDefaultAsync(b => b.Id == id);
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
            if (bookInstance.Author != null && bookInstance.Author.Id!=book.AuthorId)
            {
                var author = await _context.Authors!.FindAsync(book.AuthorId);
                if (author!=null)
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
            return _mapper.Map<Book, BookDtoShort>(bookInstance);
        return null;
    }

    public async Task<bool> Delete(int id)
    {
        var bookInstance = await _context.Books!.SingleOrDefaultAsync(b => b.Id == id);
        if (bookInstance == null) return false;
        _context.Books!.Remove(bookInstance);
        return await _context.SaveChangesAsync() > 0;
    }
}