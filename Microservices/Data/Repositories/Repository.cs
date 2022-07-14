using DataUtils;
using Microsoft.EntityFrameworkCore;
using Services.Platforms.Data.Context;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Platforms.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        public Repository(DbContext context)
        {
            this.context = context;
            this.set = context.Set<T>();
        } 

        private readonly DbContext context;
        private readonly DbSet<T> set;

        public bool Autosave { get; set; }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await context.SaveChangesAsync(cancellationToken);
        }

        public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var result = set.Add(entity);

            if (Autosave)
            {
                await context.SaveChangesAsync(cancellationToken);
            }

            return result.Entity;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var entityToDelete = await GetAsync(id);
            if(entityToDelete == null)
            {
                throw new Exception($"Cannot find entity with id={id}");
            }

            set.Remove(entityToDelete);

            if (Autosave)
            {
                await context.SaveChangesAsync(cancellationToken);
            }
        }

        public IEnumerable<T> GetAll()
        {
            return set;
        }

        public async Task<T> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            return await set.FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);
        }

        public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            if(entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var result = set.Update(entity);

            if (Autosave)
            {
                await context.SaveChangesAsync(cancellationToken);
            }

            return result.Entity;
        }
    }
}
