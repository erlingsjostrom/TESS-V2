using System.Collections.Generic;
using System.Linq;

namespace TestRestfulAPI.Infrastructure.Repositories
{
    public interface IRepository<TEntity, in TKey, in TResource> where TEntity : class
    {
        IEnumerable<IQueryable<TEntity>> All();
        ResultSet<IQueryable<TEntity>> AllWithResourceContext();
        TEntity Get(TResource resource, TKey id);
        ResultSet<TEntity> GetWithResourceContext(TResource resource, TKey id);

    }
}