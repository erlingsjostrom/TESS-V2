using System.Linq;
using System.Web.OData;
using TestRestfulAPI.Infrastructure.Entities;

namespace TestRestfulAPI.Infrastructure.Services
{
    public interface ISingleService<TEntity, in TKey> where TEntity : BaseEntity
    {
        IQueryable<TEntity> All();
        TEntity Get(TKey id);
        TEntity Create(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity PartialUpdate(TKey id, Delta<TEntity> entity);
        void Delete(TKey id);
    }
}