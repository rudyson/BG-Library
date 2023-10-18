using BGLibrary.Library.Models.Entities;

namespace BGLibrary.Library.Repositories.Interfaces;

public interface IAuthorRepository : IGenericRepository<Author>
{
    Task<IEnumerable<Book>> GetBooks(int authorId);
    Task<bool> AddBook(int authorId, int bookId);
}