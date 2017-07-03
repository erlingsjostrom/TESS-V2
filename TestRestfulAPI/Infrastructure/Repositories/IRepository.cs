using System.Collections.Generic;
using System.Linq;

namespace TestRestfulAPI.Infrastructure.Repositories
{
    public interface IRepository<TEntity, in TKey> where TEntity : class
    {
        IEnumerable<IQueryable<TEntity>> Get();
        ResultSet<IQueryable<TEntity>> GetWithResourceContext();
        IEnumerable<IQueryable<TEntity>> Get(TKey id);
        ResultSet<IQueryable<TEntity>> GetWithResourceContext(TKey id);
    }
}