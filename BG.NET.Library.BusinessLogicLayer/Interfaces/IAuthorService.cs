using BG.NET.Library.Models.Dto.Library;

namespace BG.NET.Library.BusinessLogicLayer.Interfaces;

public interface IAuthorService
{
    // HTTP.GET {void} => List<AuthorDtoNoBooks>
    public Task<IEnumerable<AuthorDtoNoBooks>> AllShort();
    public Task<IEnumerable<AuthorDtoFull>> AllFull();
    // HTTP.GET {id} => AuthorDtoNoBooks
    public Task<AuthorDtoNoBooks> FindShort();
    public Task<AuthorDtoFull> FindFull();
    // HTTP.POST {void, AuthorDtoNoId} => bool
    public Task<AuthorDtoNoBooks> Create(AuthorDtoBase author);
    // HTTP.PUT {id, AuthorDtoNoIdAndBooks}
    public Task<AuthorDtoNoBooks> Update(int id, AuthorDtoBase author);
    // HTTP.DELETE {id} => Bool
    public Task<bool> Delete(int id);

    // HTTP.GET {id} => List<BookDtoNoAuthor>
    // Similar to FindFull but Author specified
    public Task<AuthorDtoFull> Books(int id);
}