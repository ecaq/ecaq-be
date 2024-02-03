using Ecaq.Data;
using Ecaq.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecaq.Services.Repositories;


public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    internal DbSet<T> dbSet;
    protected readonly ILogger? _logger;

    public Repository(AppDbContext context)
    {
        _context = context;
        dbSet = context.Set<T>();
        //_logger = logger;
    }

    protected void Save() => _context.SaveChanges();
    protected Task SaveAsync()
    {
        return _context.SaveChangesAsync();
    }

    public int Count(Func<T, bool> predicate)
    {
        return dbSet.Where(predicate).Count();
    }

    public void Create(T entity)
    {
        dbSet.Add(entity);
        Save();
    }
    public async Task CreateAsync(T entity)
    {
        dbSet.Add(entity);
        await SaveAsync();
    }

    public void Delete(T entity)
    {
        dbSet.Remove(entity);
        Save();
    }
    public void Truncate()
    {
        _context.Database.ExecuteSqlRaw("TRUNCATE TABLE LoggedUsers");
    }

    public IEnumerable<T> Find(Func<T, bool> predicate)
    {
        return dbSet.Where(predicate);
    }

    public IEnumerable<T> GetAll()
    {
        return dbSet.ToList();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await dbSet.ToListAsync();
    }

    public T GetById(int id)
    {
        return dbSet.Find(id)!;
    }
    public T GetByGuid(Guid id)
    {
        return dbSet.Find(id)!;
    }

    public void Update(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        Save();
        _context.Entry(entity).State = EntityState.Detached;
    }
    public async Task UpdateAsync(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await SaveAsync();
        _context.Entry(entity).State = EntityState.Detached;
    }
}
