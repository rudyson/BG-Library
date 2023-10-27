using BG.NET.Library.Models.Dto.Library;

namespace BG.NET.Library.BusinessLogicLayer.Interfaces;

public interface IBookService
{
    // HTTP.POST {void} => BookDtoNew
    public Task<int> Create(BookDtoNew book);
    // HTTP.GET {id} => BookDtoAuthorId
    public Task<BookDtoShort> FindShort(int id);
    public Task<BookDtoFull> FindFull(int id);
    // HTTP.GET {void} => List<BookDtoAuthorId>
    public Task<IEnumerable<BookDtoShort>> AllShort();
    public Task<IEnumerable<BookDtoFull>> AllFull();
    // HTTP.PUT {id} => BookDtoShort
    public Task<BookDtoShort> Update(int id, BookDtoNew book);
    // HTTP.DELETE {id} => Bool
    public Task<bool> Delete(int id);
}