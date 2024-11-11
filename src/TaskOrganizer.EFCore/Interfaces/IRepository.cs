using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskOrganizer.Repository.Interfaces;

public interface IRepository<E, T> 
{
    Task<IEnumerable<E>> GetAllAsync();
    Task<E> GetAsync(T id);
    Task<E> CreateAsync(E entity);
    Task<E> UpdateAsync(E entity);
    Task<bool> DeleteAsync(T id);
}
