using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TaskOrganizer.Repository.Interfaces;
public interface IRepository<E, T> 
{
    Task<IEnumerable<E>> GetAllAsync();
    Task<IEnumerable<E>> GetAllAsync(Expression<Func<E, bool>> predicate);
    Task<E> GetAsync(T id);
    Task<E> GetAsync(Expression<Func<E, bool>> predicate);
    Task<E> CreateAsync(E entity);
    Task<E> UpdateAsync(E entity);
    Task<bool> DeleteAsync(T id);
}
