using System.Linq;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Routing;
using Microsoft.Web.Http;
using TestRestfulAPI.Infrastructure.Authorization.Attributes;
using TestRestfulAPI.Infrastructure.Controllers;
using TestRestfulAPI.RestApi.odata.v1.Users.Entities;
using TestRestfulAPI.RestApi.odata.v1.Users.Services;

namespace TestRestfulAPI.RestApi.odata.v1.Users.Controllers
{
    [ApiVersion("1.0")]
    [ODataRoutePrefix("Resources")]
    [UserHasRole("UserControl")]
    public class ResourceController : ResourceODataController, ICrudController<Resource>
    {
        private readonly ResourceService _resourceService = GlobalServices.ResourceService;

        // GET: {resource}/Resources
        [EnableQuery, HttpGet, ODataRoute("()")]
        public IQueryable<Resource> Get()
        {
            return this._resourceService.All();
        }

        // GET: {resource}/Resources({id})
        [EnableQuery, HttpGet, ODataRoute("({id})")]
        public Resource Get(int id)
        {
            return this._resourceService.Get(id);
        }

        // POST: {resource}/Resources()
        [EnableQuery, HttpPost, ODataRoute("()")]
        public IHttpActionResult Create(Resource entity)
        {
            return ODataCreated(this._resourceService.Create(entity), entity.Id);
        }

        // PUT: {resource}/Resources({id})
        [EnableQuery, HttpPut, ODataRoute("({id})")]
        public Resource Update(int id, Resource entity)
        {
            return this._resourceService.Update(entity);
        }

        // PATCH: {resource}/Resources({id})
        [EnableQuery, HttpPatch, ODataRoute("({id})")]
        public Resource PartialUpdate(int id, Delta<Resource> entity)
        {
            return this._resourceService.PartialUpdate(id, entity);
        }

        // DELETE: {resource}/Resources({id})
        [EnableQuery, HttpDelete, ODataRoute("({id})")]
        public void Delete(int id)
        {
            this.ParseResource();
            this._resourceService.Delete(id);
            this.ODataDeleted(); // Set response headers
        }
    }
}