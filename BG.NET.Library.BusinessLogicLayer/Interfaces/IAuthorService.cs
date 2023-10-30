using BG.NET.Library.Models;
using BG.NET.Library.Models.Dto.Library;

namespace BG.NET.Library.BusinessLogicLayer.Interfaces;

public interface IAuthorService
{
    // HTTP.GET {void} => List<AuthorDtoNoBooks>
    public Task<IEnumerable<AuthorDtoNoBooks>?> AllShort();
    public Task<IEnumerable<AuthorDtoFull>?> AllFull();
    public Task<GenericPaginationModel<AuthorDtoFull>?> AllPaginatedFull(int page, int size);
    // HTTP.GET {id} => AuthorDtoNoBooks
    public Task<AuthorDtoNoBooks?> FindShort(int id);
    public Task<AuthorDtoFull?> FindFull(int id);
    // HTTP.POST {void, AuthorDtoNoId} => bool
    public Task<AuthorDtoNoBooks?> Create(AuthorDtoBase author);
    // HTTP.PUT {id, AuthorDtoNoIdAndBooks}
    public Task<AuthorDtoNoBooks?> Update(int id, AuthorDtoUpdate author);
    // HTTP.DELETE {id} => Bool
    public Task<bool> Delete(int id);

    // HTTP.GET {id} => List<BookDtoNoAuthor>
    // Similar to FindFull but Author specified
    public Task<AuthorDtoFull?> Books(int id);
}