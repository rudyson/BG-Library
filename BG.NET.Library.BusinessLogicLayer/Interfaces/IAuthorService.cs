using BG.NET.Library.Models.Dto;
using BG.NET.Library.Models.Generic;
using BG.NET.Library.Models.Requests;

namespace BG.NET.Library.BusinessLogic.Interfaces;

public interface IAuthorService
{
    public Task<IEnumerable<AuthorFullInfoDto>?> AllFull();
    public Task<GenericPaginationModel<AuthorFullInfoDto>?> AllPaginatedFull(int page, int size);
    public Task<AuthorShortInfoDto?> FindShort(int id);
    public Task<AuthorFullInfoDto?> FindFull(int id);
    public Task<AuthorShortInfoDto?> Create(AuthorCreateRequest author);
    public Task<AuthorShortInfoDto?> Update(int id, AuthorUpdateRequest author);
    public Task<bool> Delete(int id);
    public Task<bool> Exists(int id);
    public Task<AuthorFullInfoDto?> Books(int id);
    public IEnumerable<AuthorAutocompleteDto>? Search(string query);
}