using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataUtils
{
    public interface IRepository<T> where T : class, IEntity
    {
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        public Task<T> GetAsync(int id, CancellationToken cancellationToken = default);
        public IEnumerable<T> GetAll();
        public Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default);
        public Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);
        public Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
