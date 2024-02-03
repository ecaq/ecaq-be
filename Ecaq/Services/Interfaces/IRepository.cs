namespace Ecaq.Services.Interfaces;

public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll();

    Task<IEnumerable<T>> GetAllAsync();

    IEnumerable<T> Find(Func<T, bool> predicate);

    T GetById(int id);

    T GetByGuid(Guid id);

    void Create(T entity);
    Task CreateAsync(T entity);

    void Update(T entity);

    Task UpdateAsync(T entity);


    void Delete(T entity);
    void Truncate();

    int Count(Func<T, bool> predicate);
}
