using BG.NET.Library.Models.Dto;
using BG.NET.Library.Models.Generic;
using BG.NET.Library.Models.Requests;

namespace BG.NET.Library.BusinessLogic.Interfaces;

public interface IBookService
{
    public Task<BookFullInfoDto?> Create(BookCreateRequest book);
    public Task<BookFullInfoDto?> FindFull(int id);
    public Task<IEnumerable<BookFullInfoDto>?> AllFull();
    public Task<GenericPaginationModel<BookFullInfoDto>?> AllPaginatedFull(int page, int size);
    public Task<BookFullInfoDto?> Update(int id, BookUpdateRequest book);
    public Task<bool> Delete(int id);
    public Task<bool> Exists(int id);
}