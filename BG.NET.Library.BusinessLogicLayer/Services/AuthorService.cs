using AutoMapper;
using BG.NET.Library.BusinessLogicLayer.Interfaces;
using BG.NET.Library.DataAccessLayer.Contexts;
using BG.NET.Library.DataAccessLayer.Interfaces;
using BG.NET.Library.Models.Dto.Library;
using BG.NET.Library.Models.Entities.Library;
using Microsoft.EntityFrameworkCore;

namespace BG.NET.Library.BusinessLogicLayer.Services;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _repository;
    private readonly IMapper _mapper;
    private readonly LibraryDbContext _context;

    public AuthorService(IAuthorRepository repository, IMapper mapper, LibraryDbContext context)
    {
        _repository = repository;
        _mapper = mapper;
        _context = context;
    }
    public async Task<IEnumerable<AuthorDtoNoBooks>?> AllShort()
    {
        var authors = await _context.Authors!.Include(a => a.Books).ToListAsync();
        return _mapper.Map<List<Author>, List<AuthorDtoNoBooks>>(source:authors);
    }

    public async Task<IEnumerable<AuthorDtoFull>?> AllFull()
    {
        var authors = await _context.Authors!.Include(a => a.Books).ToListAsync();
        return _mapper.Map<List<Author>, List<AuthorDtoFull>>(source:authors);
    }

    public async Task<AuthorDtoNoBooks?> FindShort(int id)
    {
        var author = await _context.Authors!.Include(a => a.Books).SingleOrDefaultAsync(a => a.Id == id);
        return author == null
            ? null
            : _mapper.Map<Author, AuthorDtoNoBooks>(source:author);
    }

    public async Task<AuthorDtoFull?> FindFull(int id)
    {
        var author = await _context.Authors!.Include(a => a.Books).SingleOrDefaultAsync(a => a.Id == id);
        return author == null
            ? null
            : _mapper.Map<Author, AuthorDtoFull>(source:author);
    }

    public async Task<AuthorDtoNoBooks?> Create(AuthorDtoBase author)
    {
        var mappedAuthorToCreate = _mapper.Map<AuthorDtoBase, Author>(author);
        var createdAuthor = await _context.Authors!.AddAsync(mappedAuthorToCreate);
        return
            await _context.SaveChangesAsync() > 0
                ? _mapper.Map<Author, AuthorDtoNoBooks>(createdAuthor.Entity)
                : null;
    }

    public async Task<AuthorDtoNoBooks?> Update(int id, AuthorDtoUpdate author)
    {
        var authorInstance = await _context.Authors!.SingleOrDefaultAsync(a => a.Id == id);
        if (authorInstance == null) return null;
        var entryChanged = false;

        if (author.Birthday.HasValue && authorInstance.Birthday != author.Birthday)
        {
            authorInstance.Birthday = author.Birthday.Value;
            entryChanged = true;
        }

        if (author.Name != null && authorInstance.Name != author.Name)
        {
            authorInstance.Name = author.Name;
            entryChanged = true;
        }

        if (author.Surname != null && authorInstance.Surname != author.Surname)
        {
            authorInstance.Surname = author.Surname;
            entryChanged = true;
        }

        if (entryChanged && await _context.SaveChangesAsync() > 0)
            return _mapper.Map<Author, AuthorDtoNoBooks>(authorInstance);
        return null;
    }

    public async Task<bool> Delete(int id)
    {
        var authorInstance = await _context.Authors!.SingleOrDefaultAsync(a => a.Id == id);
        if (authorInstance == null) return false;
        _context.Authors!.Remove(authorInstance);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<AuthorDtoFull?> Books(int id)
    {
        var authorInstance = await _context.Authors!.Include(a=>a.Books).SingleOrDefaultAsync(a => a.Id == id);
        return authorInstance == null
            ? null
            : _mapper.Map<Author, AuthorDtoFull>(authorInstance);
    }
}