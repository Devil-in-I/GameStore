using Data.Data;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity: class
    {
        private readonly GameStoreDbContext context;
        public Repository(GameStoreDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(TEntity entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            await context.Set<TEntity>().AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            context.Set<TEntity>().Remove(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await context.Set<TEntity>().FindAsync(id);
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            context.Set<TEntity>().Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            var entity = await context.Set<TEntity>().FindAsync(id);
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            return entity;
        }

        public void Update(TEntity entity)
        {
            context.Set<TEntity>().Update(entity);
        }
    }
}
