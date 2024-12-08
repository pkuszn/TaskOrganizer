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

    public async Task<E> GetAsync(T id, CancellationToken cancellationToken)
        => await DbContext.Set<E>().FirstOrDefaultAsync(e => e.Id.Equals(id), cancellationToken: cancellationToken);

    public async Task<IEnumerable<E>> GetAllAsync(CancellationToken cancellationToken)
        => await DbContext.Set<E>().ToListAsync(cancellationToken);

    public async Task<E> GetAsync(Expression<Func<E, bool>> predicate, CancellationToken cancellationToken)
        => await DbContext.Set<E>().FirstOrDefaultAsync(predicate, cancellationToken);

    public async Task<IEnumerable<E>> GetAllAsync(Expression<Func<E, bool>> predicate, CancellationToken cancellationToken)
        => await DbContext.Set<E>().Where(predicate).ToListAsync(cancellationToken);

    public async Task<E> CreateAsync(E entity, CancellationToken cancellationToken)
    {
        Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<E> newEntity = await DbContext.Set<E>().AddAsync(entity, cancellationToken);
        await DbContext.SaveChangesAsync(cancellationToken);
        return newEntity.Entity;
    }

    public async Task<bool> DeleteAsync(T id, CancellationToken cancellationToken)
    {
        E entity = await DbContext.Set<E>().FirstOrDefaultAsync(e => e.Id.Equals(id), cancellationToken);
        DbContext.Set<E>().Remove(entity);
        await DbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<E> UpdateAsync(E entity, CancellationToken cancellationToken)
    {
        DbContext.Set<E>().Update(entity);
        await DbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }
}