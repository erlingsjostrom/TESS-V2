using System.Collections.Generic;
using System.Linq;

namespace TestRestfulAPI.Infrastructure.Repositories
{
    public interface ISingleRepository<TEntity, in TKey> where TEntity : class 
    {
        IQueryable<TEntity> All();
        TEntity Get(TKey id);
        TEntity Update(TEntity entity);
        TEntity Create(TEntity entity);
    }
}