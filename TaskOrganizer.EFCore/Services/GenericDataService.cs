using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskOrganizer.Domain.Models;
using TaskOrganizer.Domain.Services;

namespace TaskOrganizer.EFCore.Services
{
    public class GenericDataService<T> : IDataService<T> where T : DomainObject
    {
        private readonly MyDbContextFactory _contextFactory;

        public GenericDataService(MyDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public T CreatedMethod(T entity)
        {
            using (MyDbContext context = _contextFactory.CreateDbContext())
            {
                EntityEntry<T> createdEntity = context.Set<T>().Add(entity);
                context.SaveChanges();
                return createdEntity.Entity;
            }
        }

        public async Task<T> Create(T entity)
        {
            using (MyDbContext context = _contextFactory.CreateDbContext())
            {
                var newEntity = await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync();
                return newEntity.Entity;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (MyDbContext context = _contextFactory.CreateDbContext())
            {
                T entity = await context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();
                return true;

            }
        }

        public async Task<T> Get(int id)
        {
            using (MyDbContext context = _contextFactory.CreateDbContext())
            {
                T entity = await context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
                return entity;
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            using (MyDbContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<T> entities = await context.Set<T>().ToListAsync();
                return entities;

            }
        }

        public async Task<T> Update(int id, T entity)
        {
            using (MyDbContext context = _contextFactory.CreateDbContext())
            {
                entity.Id = id;
                context.Set<T>().Update(entity);
                await context.SaveChangesAsync();
                return entity;
            }
        }
    }
}

