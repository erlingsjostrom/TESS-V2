using System.Linq;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Routing;
using Microsoft.Web.Http;
using TestRestfulAPI.Infrastructure.Controllers;
using TestRestfulAPI.RestApi.odata.v1.Users.Entities;
using TestRestfulAPI.RestApi.odata.v1.Users.Services;

namespace TestRestfulAPI.RestApi.odata.v1.Users.Controllers
{
    [ApiVersion("1.0")]
    [ODataRoutePrefix("Permissions")]
    public class PermissionController : ResourceODataController, ICrudController<Permission>
    {
        private readonly PermissionService _permissionService = GlobalServices.PermissionService;

        // GET: {resource}/Permissions
        [EnableQuery, HttpGet, ODataRoute("()")]
        public IQueryable<Permission> Get()
        {
            return this._permissionService.All();
        }

        // GET: {resource}/Permissions({id})
        [EnableQuery, HttpGet, ODataRoute("({id})")]
        public Permission Get(int id)
        {
            return this._permissionService.Get(id);
        }

        // POST: {resource}/Permissions()
        [EnableQuery, HttpPost, ODataRoute("()")]
        public IHttpActionResult Create(Permission entity)
        {
            return ODataCreated(this._permissionService.Create(entity), entity.Id);
        }

        // PUT: {resource}/Permissions({id})
        [EnableQuery, HttpPut, ODataRoute("({id})")]
        public Permission Update(int id, Permission entity)
        {
            return this._permissionService.Update(entity);
        }

        // PATCH: {resource}/Permissions({id})
        [EnableQuery, HttpPatch, ODataRoute("({id})")]
        public Permission PartialUpdate(int id, Delta<Permission> entity)
        {
            return this._permissionService.PartialUpdate(id, entity);
        }

        // DELETE: {resource}/Permissions({id})
        [EnableQuery, HttpDelete, ODataRoute("({id})")]
        public void Delete(int id)
        {
            this.ParseResource();
            this._permissionService.Delete(id);
            this.ODataDeleted(); // Set response headers
        }
    }
}