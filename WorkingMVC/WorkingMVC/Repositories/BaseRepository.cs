using Microsoft.EntityFrameworkCore;
using WorkingMVC.Data.Entities;
using WorkingMVC.Interfaces;

namespace WorkingMVC.Repositories;

public abstract class BaseRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
{
    protected readonly DbContext _dbContext;
    protected readonly DbSet<TEntity> _dbSet;

    public BaseRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<TEntity>();
    }

    public virtual async Task<TEntity?> GetByIdAsync(TKey id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(bool isDeleted = false)
    {
        return await _dbSet.Where(x=>x.IsDeleted==isDeleted).ToListAsync();
    }

    public virtual async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public virtual Task UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        return Task.CompletedTask;
    }

    public virtual Task DeleteAsync(TEntity entity)
    {
        _dbSet.Remove(entity);
        return Task.CompletedTask;
    }

    public Task<IQueryable<TEntity>> GetAllQurableAsync()
    {
        throw new NotImplementedException();
    }
}
