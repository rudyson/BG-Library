using BGLibrary.Library.Models.Entities;

namespace BGLibrary.Library.Repositories.Interfaces;

public interface IBookRepository : IGenericRepository<Book>
{
    Task<Author?> GetAuthor(int bookId);
    Task<bool> SetAuthor(int bookId,int authorId);
}