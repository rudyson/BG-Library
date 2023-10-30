using BG.NET.Library.Models;
using BG.NET.Library.Models.Dto.Library;

namespace BG.NET.Library.BusinessLogicLayer.Interfaces;

public interface IBookService
{
    // HTTP.POST {void} => BookDtoNew
    public Task<BookDtoFull?> Create(BookDtoNew book);
    // HTTP.GET {id} => BookDtoAuthorId
    public Task<BookDtoShort?> FindShort(int id);
    public Task<BookDtoFull?> FindFull(int id);
    // HTTP.GET {void} => List<BookDtoAuthorId>
    public Task<IEnumerable<BookDtoShort>?> AllShort();
    public Task<IEnumerable<BookDtoFull>?> AllFull();
    public Task<GenericPaginationModel<BookDtoFull>?> AllPaginatedFull(int page, int size);
    // HTTP.PUT {id} => BookDtoShort
    public Task<BookDtoShort?> Update(int id, BookDtoUpdate book);
    // HTTP.DELETE {id} => Bool
    public Task<bool> Delete(int id);
}