using BGLibrary.Library.Contexts;
using BGLibrary.Library.Models.Entities;
using BGLibrary.Library.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BGLibrary.Library.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly ILogger<AuthorRepository> _logger;
    private readonly  LibraryDbContext _context;
    private readonly DbSet<Author> _dbSet;
    public AuthorRepository(ILogger<AuthorRepository> logger, LibraryDbContext context)
    {
        _context = context;
        _dbSet = context.Set<Author>();
        _logger = logger;
    }

    public async Task<IEnumerable<Author>> GetAll()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<Author?> GetSingle(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<bool> Create(Author author)
    {
        await _dbSet.AddAsync(author);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Update(Author author)
    {
        var updateItem = await _dbSet.FindAsync(author.Id);
        if (updateItem == null)
            return false;
        
        if (updateItem.Birthday != author.Birthday)
            updateItem.Birthday = author.Birthday;
        if (updateItem.Name != author.Name)
            updateItem.Name = author.Name;
        if (updateItem.Surname != author.Surname)
            updateItem.Surname = author.Surname;

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

    public async Task<IEnumerable<Book>> GetBooks(int id)
    {
        var author = await _dbSet.FindAsync(id);
        return author == null ? new List<Book>() : author.Books;
    }

    public async Task<bool> AddBook(int authorId, int bookId)
    {
        var author = await _dbSet.FindAsync(authorId);
        var book = await _context.Books!.FindAsync(bookId);
        if (author == null || book == null || book.Author!=null)
            return false;
        book.Author = author;
        await _context.SaveChangesAsync();
        return true;
    }
}