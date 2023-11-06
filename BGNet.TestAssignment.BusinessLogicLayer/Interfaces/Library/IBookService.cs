using BGNet.TestAssignment.Common.WebApi.Models.Pagination;
using BGNet.TestAssignment.Models.Dto.Library;
using BGNet.TestAssignment.Models.Requests.Library;

namespace BGNet.TestAssignment.BusinessLogic.Interfaces.Library;

public interface IBookService
{
    public Task<BookShortInfoDto?> CreateAsync(BookCreateRequest book, CancellationToken cancellationToken);
    public Task<BookFullInfoDto?> FindFullAsync(int id, CancellationToken cancellationToken);
    public Task<IEnumerable<BookFullInfoDto>?> AllFullAsync(CancellationToken cancellationToken);
    public Task<GenericPaginationModel<BookFullInfoDto>?> AllPaginatedPageSizeFullAsync(int page, int size, CancellationToken cancellationToken);
    public Task<GenericPaginationModel<BookFullInfoDto>?> AllPaginatedSkipTakeFullAsync(int skip, int take, CancellationToken cancellationToken);
    public Task<BookShortInfoDto?> UpdateAsync(int id, BookUpdateRequest book, CancellationToken cancellationToken);
    public Task<BookFullInfoDto?> DeleteAsync(int id, CancellationToken cancellationToken);
    public Task<bool> ExistsAsync(int id, CancellationToken cancellationToken);
}