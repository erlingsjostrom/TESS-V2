using System.Linq;
using System.Web.OData;
using TestRestfulAPI.Infrastructure.Entities;

namespace TestRestfulAPI.Infrastructure.Services
{
    public interface IService<TEntity, in TKey, in TResource> where TEntity : BaseEntity
    {
        IQueryable<TEntity> All(TResource resource);
        TEntity Get(TResource resource, TKey id);
        TEntity Create(TResource resource, TEntity entity);
        TEntity Update(TResource resource, TEntity entity);
        TEntity PartialUpdate(TResource resource, TKey id, Delta<TEntity> entity);
        void Delete(TResource resource, TKey id);
    }
}