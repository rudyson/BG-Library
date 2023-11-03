using BGNet.TestAssignment.Common.WebApi.Models.Pagination;
using BGNet.TestAssignment.Models.Dto.Library;
using BGNet.TestAssignment.Models.Requests.Library;

namespace BGNet.TestAssignment.BusinessLogic.Interfaces.Library;

public interface IBookService
{
    public Task<BookShortInfoDto?> Create(BookCreateRequest book);
    public Task<BookFullInfoDto?> FindFull(int id);
    public Task<IEnumerable<BookFullInfoDto>?> AllFull();
    public Task<GenericPaginationModel<BookFullInfoDto>?> AllPaginatedFull(int page, int size);
    public Task<BookShortInfoDto?> Update(int id, BookUpdateRequest book);
    public Task<BookFullInfoDto?> Delete(int id);
    public Task<bool> Exists(int id);
}