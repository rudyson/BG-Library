namespace BG.NET.Library.DataAccessLayer.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAll();
    //Task<IEnumerable<T>> GetPaginated(int? page, int? pageSize);
    Task<T?> GetSingle(int id);
    Task<bool> Create(T entity);
    Task<bool> Update(T entity);
    Task<bool> Delete(int id);
}