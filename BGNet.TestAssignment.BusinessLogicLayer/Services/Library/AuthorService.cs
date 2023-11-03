using BGNet.TestAssignment.BusinessLogic.Interfaces.Library;
using BGNet.TestAssignment.Common.WebApi.Models.Pagination;
using BGNet.TestAssignment.DataAccess.Contexts;
using BGNet.TestAssignment.DataAccess.Entities;
using BGNet.TestAssignment.Models.Dto.Library;
using BGNet.TestAssignment.Models.Requests.Library;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BGNet.TestAssignment.BusinessLogic.Services.Library;

public class AuthorService : IAuthorService
{
    private readonly LibraryDbContext _context;

    public AuthorService(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AuthorFullInfoDto>?> AllFull()
    {
        var authors = await _context.Authors.Include(a => a.Books).OrderBy(a => a.Surname).ToListAsync();
        return authors.Adapt<List<AuthorFullInfoDto>>();
    }

    public async Task<GenericPaginationModel<AuthorFullInfoDto>?> AllPaginatedFull(int page, int size)
    {
        var authors = _context.Authors
            .Include(a => a.Books)
            .OrderBy(x => x.Surname);
        var total = await authors.CountAsync();
        var numberSkipped = PaginationCalculationAssistant.Skipped(page, size);
        var entities = await authors
                    .Skip(numberSkipped)
                    .Take(size)
                    .ToListAsync();
        return new GenericPaginationModel<AuthorFullInfoDto>
        {
            Page = page,
            PageSize = size,
            TotalSize = total,
            Pages = PaginationCalculationAssistant.TotalPages(total, size),
            NumberSkipped = numberSkipped,
            Entities = total > 0 ? entities.Adapt<List<AuthorFullInfoDto>>() : new List<AuthorFullInfoDto>()
        };
    }

    public async Task<AuthorShortInfoDto?> FindShort(int id)
    {
        var author = await _context.Authors.Include(a => a.Books).SingleOrDefaultAsync(a => a.Id == id);
        return author?.Adapt<AuthorShortInfoDto>();
    }

    public async Task<AuthorFullInfoDto?> FindFull(int id)
    {
        var author = await _context.Authors.Include(a => a.Books).SingleOrDefaultAsync(a => a.Id == id);
        return author?.Adapt<AuthorFullInfoDto>();
    }

    public async Task<AuthorShortInfoDto?> Create(AuthorCreateRequest author)
    {
        var mappedAuthorToCreate = author.Adapt<Author>();
        var createdAuthor = await _context.Authors.AddAsync(mappedAuthorToCreate);
        if (createdAuthor.State == EntityState.Added)
        {
            await _context.SaveChangesAsync();
            return createdAuthor.Entity.Adapt<AuthorShortInfoDto>();
        }
        return null;
    }

    public async Task<AuthorShortInfoDto?> Update(int id, AuthorUpdateRequest author)
    {
        var authorInstance = await _context.Authors.SingleOrDefaultAsync(a => a.Id == id);
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
            return authorInstance.Adapt<AuthorShortInfoDto>();
        return null;
    }

    public async Task<AuthorShortInfoDto?> Delete(int id)
    {
        var authorInstance = await _context.Authors.SingleOrDefaultAsync(a => a.Id == id);
        if (authorInstance == null) return null;
        _context.Authors.Remove(authorInstance);
        if (await _context.SaveChangesAsync() > 0)
            return authorInstance.Adapt<AuthorShortInfoDto?>();
        return null;
    }

    public async Task<AuthorFullInfoDto?> Books(int id)
    {
        var authorInstance = await _context.Authors.Include(a => a.Books).SingleOrDefaultAsync(a => a.Id == id);
        return authorInstance?.Adapt<AuthorFullInfoDto>();
    }

    public IEnumerable<AuthorAutocompleteDto>? Search(string query)
    {
        if (query.IsNullOrEmpty()) return null;
        var queryLower = query.ToLower();
        return _context.Authors
            .Where(a => a.Surname.ToLower().Contains(queryLower) || a.Name.ToLower().Contains(queryLower))
            .Take(5)
            .Adapt<IEnumerable<AuthorAutocompleteDto>?>();
    }
    public async Task<bool> Exists(int id) => await _context.Authors.SingleOrDefaultAsync(a => a.Id == id) != null;
}