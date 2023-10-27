using BG.NET.Library.DataAccessLayer.Contexts;
using BG.NET.Library.DataAccessLayer.Interfaces;
using BG.NET.Library.Models.Entities.Library;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BG.NET.Library.DataAccessLayer.Repositories;

public class BookRepository : IBookRepository
{
    private readonly ILogger<BookRepository> _logger;
    private readonly  LibraryDbContext _context;
    private readonly DbSet<Book> _dbSet;

    public BookRepository(
        LibraryDbContext context,
        ILogger<BookRepository> logger
    )
    {
        _context = context;
        _logger = logger;
        _dbSet = context.Set<Book>();
    }
    
    public async Task<IEnumerable<Book>> GetAll()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<Book?> GetSingle(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<bool> Create(Book book)
    {
        await _dbSet.AddAsync(book);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Update(Book book)
    {
        var updateItem = await _dbSet.FindAsync(book.Id);
        if (updateItem == null)
            return false;
        
        if (updateItem.Title != book.Title)
            updateItem.Title = book.Title;
        if (updateItem.PublishYear != book.PublishYear)
            updateItem.PublishYear = book.PublishYear;
        if (updateItem.Genre != book.Genre)
            updateItem.Genre = book.Genre;
        if (updateItem.Author != book.Author)
            updateItem.Author = book.Author;
        
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var removalItem = await _dbSet.FindAsync(id);
        if (removalItem == null)
            return false;
        _dbSet.Remove(removalItem);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Author?> GetAuthor(int id)
    {
        var book = await _dbSet.FindAsync(id);
        return book?.Author;
    }

    public async Task<bool> SetAuthor(int bookId, int authorId)
    {
        var book = await _dbSet.FindAsync(bookId);
        var author = await _context.Authors!.FindAsync(authorId);
        if (author == null || book == null || book.Author!=null)
            return false;
        book.Author = author;
        await _context.SaveChangesAsync();
        return true;
    }
}