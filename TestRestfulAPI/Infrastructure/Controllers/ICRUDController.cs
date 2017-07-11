using System.Linq;
using System.Web.Http;
using System.Web.OData;
using TestRestfulAPI.Entities.User;

namespace TestRestfulAPI.Infrastructure.Controllers
{
    public interface ICrudController<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// HttpGet All entities
        /// </summary>
        /// <returns>collection of all entities, 200 OK</returns>
        IQueryable<TEntity> Get();

        /// <summary>
        /// HttpGet Entity by id
        /// </summary>
        /// <param name="id">id to select</param>
        /// <returns>entity with provided entity, 200 OK</returns>
        TEntity Get(int id);

        /// <summary>
        /// HttpPost Create new entity
        /// </summary>
        /// <param name="entity">new entity to create</param>
        /// <returns>new entity, 201 Created response status code and correct Location header of new entity</returns>
        IHttpActionResult Create([FromBody] TEntity entity);

        /// <summary>
        /// HttpPut Replace entity at id
        /// </summary>
        /// <param name="id">it to select</param>
        /// <param name="entity">entity to replace with</param>
        /// <returns>new entity, 200 OK</returns>
        TEntity Update(int id, [FromBody] TEntity entity);

        /// <summary>
        /// HttpPatch Partial entity update
        /// </summary>
        /// <param name="id">id to select</param>
        /// <param name="entity">partial entity to update with</param>
        /// <returns>updated entity, 200 OK</returns>
        TEntity PartialUpdate(int id, [FromBody] Delta<TEntity> entity);

        /// <summary>
        /// HttpDelete Entity at id
        /// </summary>
        /// <param name="id">id to select</param>
        /// <returns>204 No Content</returns>
        void Delete(int id);
    }
}