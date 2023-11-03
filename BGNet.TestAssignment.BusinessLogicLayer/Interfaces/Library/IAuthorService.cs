using BGNet.TestAssignment.Common.WebApi.Models.Pagination;
using BGNet.TestAssignment.Models.Dto.Library;
using BGNet.TestAssignment.Models.Requests.Library;

namespace BGNet.TestAssignment.BusinessLogic.Interfaces.Library;

public interface IAuthorService
{
    public Task<IEnumerable<AuthorFullInfoDto>?> AllFull();
    public Task<GenericPaginationModel<AuthorFullInfoDto>?> AllPaginatedFull(int page, int size);
    public Task<AuthorShortInfoDto?> FindShort(int id);
    public Task<AuthorFullInfoDto?> FindFull(int id);
    public Task<AuthorShortInfoDto?> Create(AuthorCreateRequest author);
    public Task<AuthorShortInfoDto?> Update(int id, AuthorUpdateRequest author);
    public Task<AuthorShortInfoDto?> Delete(int id);
    public Task<bool> Exists(int id);
    public Task<AuthorFullInfoDto?> Books(int id);
    public IEnumerable<AuthorAutocompleteDto>? Search(string query);
}