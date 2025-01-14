using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TaskOrganizer.Domain.Interfaces;
using TaskOrganizer.Repository.Interfaces;

namespace TaskOrganizer.Repository;

public class BaseRepository<E, T> : IRepository<E, T>
    where E : class, IObject<T>
    where T : struct
{
    private readonly TaskOrganizerDbContext DbContext;

    public BaseRepository(TaskOrganizerDbContext dbContext)
    {
        DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<E> GetAsync(T id)
        => await DbContext.Set<E>().FirstOrDefaultAsync(e => e.Id.Equals(id));

    public async Task<IEnumerable<E>> GetAllAsync()
        => await DbContext.Set<E>().ToListAsync();

    public async Task<E> GetAsync(Expression<Func<E, bool>> predicate)
        => await DbContext.Set<E>().FirstOrDefaultAsync(predicate);

    public async Task<IEnumerable<E>> GetAllAsync(Expression<Func<E, bool>> predicate)
        => await DbContext.Set<E>().Where(predicate).ToListAsync();

    public async Task<E> CreateAsync(E entity)
    {
        Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<E> newEntity = await DbContext.Set<E>().AddAsync(entity);
        await DbContext.SaveChangesAsync();
        return newEntity.Entity;
    }

    public async Task<bool> DeleteAsync(T id)
    {
        E entity = await DbContext.Set<E>().FirstOrDefaultAsync(e => e.Id.Equals(id));
        DbContext.Set<E>().Remove(entity);
        await DbContext.SaveChangesAsync();
        return true;
    }

    public async Task<E> UpdateAsync(E entity)
    {
        DbContext.Set<E>().Update(entity);
        await DbContext.SaveChangesAsync();
        return entity;
    }
}