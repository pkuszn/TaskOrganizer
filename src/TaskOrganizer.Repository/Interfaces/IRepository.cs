using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace TaskOrganizer.Repository.Interfaces;

public interface IRepository<E, T> 
{
    Task<IEnumerable<E>> GetAllAsync(CancellationToken cancellationToken);
    Task<IEnumerable<E>> GetAllAsync(Expression<Func<E, bool>> predicate, CancellationToken cancellationToken);
    Task<E> GetAsync(T id, CancellationToken cancellationToken);
    Task<E> GetAsync(Expression<Func<E, bool>> predicate, CancellationToken cancellationToken);
    Task<E> CreateAsync(E entity, CancellationToken cancellationToken);
    Task<E> UpdateAsync(E entity, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(T id, CancellationToken cancellationToken);
}
