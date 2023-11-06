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

    public async Task<IEnumerable<AuthorFullInfoDto>?> AllFullAsync(CancellationToken cancellationToken)
    {
        var authors = await _context.Authors.Include(a => a.Books).OrderBy(a => a.Surname).ToListAsync(cancellationToken: cancellationToken);
        return authors.Adapt<List<AuthorFullInfoDto>>();
    }

    public async Task<GenericPaginationModel<AuthorFullInfoDto>?> AllPaginatedPageSizeFullAsync(int page, int size, CancellationToken cancellationToken)
    {
        var authors = _context.Authors
            .Include(a => a.Books)
            .OrderBy(x => x.Surname);
        var total = await authors.CountAsync(cancellationToken: cancellationToken);
        var numberSkipped = PaginationCalculationAssistant.Skipped(page, size);
        var entities = await authors
                    .Skip(numberSkipped)
                    .Take(size)
                    .ToListAsync(cancellationToken: cancellationToken);
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
    public async Task<GenericPaginationModel<AuthorFullInfoDto>?> AllPaginatedSkipTakeFullAsync(int skip, int take, CancellationToken cancellationToken)
    {
        var authors = _context.Authors
            .Include(a => a.Books)
            .OrderBy(x => x.Surname);
        var total = await authors.CountAsync(cancellationToken: cancellationToken);
        var entities = await authors
                    .Skip(skip)
                    .Take(take)
                    .ToListAsync(cancellationToken: cancellationToken);
        return new GenericPaginationModel<AuthorFullInfoDto>
        {
            Page = PaginationCalculationAssistant.CurrentPageSkipTake(skip, take),
            PageSize = take,
            TotalSize = total,
            Pages = PaginationCalculationAssistant.TotalPages(total, take),
            NumberSkipped = skip,
            Entities = total > 0 ? entities.Adapt<List<AuthorFullInfoDto>>() : new List<AuthorFullInfoDto>()
        };
    }

    public async Task<AuthorShortInfoDto?> FindShortAsync(int id, CancellationToken cancellationToken)
    {
        var author = await _context.Authors.Include(a => a.Books).SingleOrDefaultAsync(a => a.Id == id, cancellationToken: cancellationToken);
        return author?.Adapt<AuthorShortInfoDto>();
    }

    public async Task<AuthorFullInfoDto?> FindFullAsync(int id, CancellationToken cancellationToken)
    {
        var author = await _context.Authors.Include(a => a.Books).SingleOrDefaultAsync(a => a.Id == id, cancellationToken: cancellationToken);
        return author?.Adapt<AuthorFullInfoDto>();
    }

    public async Task<AuthorShortInfoDto?> CreateAsync(AuthorCreateRequest author, CancellationToken cancellationToken)
    {
        var mappedAuthorToCreate = author.Adapt<Author>();
        var createdAuthor = await _context.Authors.AddAsync(mappedAuthorToCreate, cancellationToken: cancellationToken);
        if (createdAuthor.State == EntityState.Added)
        {
            await _context.SaveChangesAsync(cancellationToken: cancellationToken);
            return createdAuthor.Entity.Adapt<AuthorShortInfoDto>();
        }
        return null;
    }

    public async Task<AuthorShortInfoDto?> UpdateAsync(int id, AuthorUpdateRequest author, CancellationToken cancellationToken)
    {
        var authorInstance = await _context.Authors.SingleOrDefaultAsync(a => a.Id == id, cancellationToken: cancellationToken);
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

        if (entryChanged && await _context.SaveChangesAsync(cancellationToken: cancellationToken) > 0)
            return authorInstance.Adapt<AuthorShortInfoDto>();
        return null;
    }

    public async Task<AuthorShortInfoDto?> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var authorInstance = await _context.Authors.SingleOrDefaultAsync(a => a.Id == id, cancellationToken: cancellationToken);
        if (authorInstance == null) return null;
        _context.Authors.Remove(authorInstance);
        if (await _context.SaveChangesAsync(cancellationToken: cancellationToken) > 0)
            return authorInstance.Adapt<AuthorShortInfoDto?>();
        return null;
    }

    public async Task<AuthorFullInfoDto?> BooksAsync(int id, CancellationToken cancellationToken)
    {
        var authorInstance = await _context.Authors.Include(a => a.Books).SingleOrDefaultAsync(a => a.Id == id, cancellationToken: cancellationToken);
        return authorInstance?.Adapt<AuthorFullInfoDto>();
    }

    public IEnumerable<AuthorAutocompleteDto>? SearchAsync(string query, CancellationToken cancellationToken)
    {
        if (query.IsNullOrEmpty()) return null;
        var queryLower = query.ToLower();
        return _context.Authors
            .Where(a => a.Surname.ToLower().Contains(queryLower) || a.Name.ToLower().Contains(queryLower))
            .Take(5)
            .Adapt<IEnumerable<AuthorAutocompleteDto>?>();
    }
    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken) => await _context.Authors.SingleOrDefaultAsync(a => a.Id == id, cancellationToken: cancellationToken) != null;
}