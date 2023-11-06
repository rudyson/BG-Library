using BGNet.TestAssignment.Common.WebApi.Models.Pagination;
using BGNet.TestAssignment.Models.Dto.Library;
using BGNet.TestAssignment.Models.Requests.Library;

namespace BGNet.TestAssignment.BusinessLogic.Interfaces.Library;

public interface IAuthorService
{
    public Task<IEnumerable<AuthorFullInfoDto>?> AllFullAsync(CancellationToken cancellationToken);
    public Task<GenericPaginationModel<AuthorFullInfoDto>?> AllPaginatedPageSizeFullAsync(int page, int size, CancellationToken cancellationToken);
    public Task<GenericPaginationModel<AuthorFullInfoDto>?> AllPaginatedSkipTakeFullAsync(int skip, int take, CancellationToken cancellationToken);
    public Task<AuthorShortInfoDto?> FindShortAsync(int id, CancellationToken cancellationToken);
    public Task<AuthorFullInfoDto?> FindFullAsync(int id, CancellationToken cancellationToken);
    public Task<AuthorShortInfoDto?> CreateAsync(AuthorCreateRequest author, CancellationToken cancellationToken);
    public Task<AuthorShortInfoDto?> UpdateAsync(int id, AuthorUpdateRequest author, CancellationToken cancellationToken);
    public Task<AuthorShortInfoDto?> DeleteAsync(int id, CancellationToken cancellationToken);
    public Task<bool> ExistsAsync(int id, CancellationToken cancellationToken);
    public Task<AuthorFullInfoDto?> BooksAsync(int id, CancellationToken cancellationToken);
    public IEnumerable<AuthorAutocompleteDto>? SearchAsync(string query, CancellationToken cancellationToken);
}