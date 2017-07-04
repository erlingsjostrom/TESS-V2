using System.Collections.Generic;
using System.Linq;

namespace TestRestfulAPI.Infrastructure.Repositories
{
    public interface IRepository<TEntity, in TKey, in TResource> where TEntity : class
    {
        IEnumerable<IQueryable<TEntity>> All();
        ResultSet<IQueryable<TEntity>> AllWithResourceContext();
        IEnumerable<IQueryable<TEntity>> Get(TKey id, TResource resource);
        ResultSet<TEntity> GetWithResourceContext(TKey id, TResource resource);

    }
}