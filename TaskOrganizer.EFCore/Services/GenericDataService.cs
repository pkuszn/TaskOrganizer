using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskOrganizer.Domain.Models;
using TaskOrganizer.Domain.Services;

namespace TaskOrganizer.EFCore.Services
{
    public class GenericDataService<T> : IDataService<T> where T : DomainObject
    {
        private readonly MyDbContextFactory ContextFactory;

        public GenericDataService(MyDbContextFactory contextFactory)
        {
            ContextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
        }

        public async Task<T> Create(T entity)
        {
            using MyDbContext context = ContextFactory.CreateDbContext();
            Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<T> newEntity = await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
            return newEntity.Entity;
        }

        public async Task<bool> Delete(int id)
        {
            using MyDbContext context = ContextFactory.CreateDbContext();
            T entity = await context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<T> Get(int id)
        {
            using MyDbContext context = ContextFactory.CreateDbContext();
            T entity = await context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
            return entity;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            using MyDbContext context = ContextFactory.CreateDbContext();
            IEnumerable<T> entities = await context.Set<T>().Where(entities => entities.IsSelected == true).ToListAsync();
            return entities;
        }

        public async Task<T> Update(int id, T entity)
        {
            using MyDbContext context = ContextFactory.CreateDbContext();
            entity.Id = id;
            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }
    }
}

