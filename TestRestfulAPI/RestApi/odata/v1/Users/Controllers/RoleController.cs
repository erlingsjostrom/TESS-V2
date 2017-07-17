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
    [ODataRoutePrefix("Roles")]
    [UserHasRole("UserControl")]
    public class RoleController : ResourceODataController, ICrudController<Role>
    {
        private readonly RoleService _roleService = GlobalServices.RoleService;

        // GET: {resource}/Roles
        [EnableQuery, HttpGet, ODataRoute("()")]
        public IQueryable<Role> Get()
        {
            return this._roleService.All();
        }

        // GET: {resource}/Roles({id})
        [EnableQuery, HttpGet, ODataRoute("({id})")]
        public Role Get(int id)
        {
            return this._roleService.Get(id);
        }

        // POST: {resource}/Roles()
        [EnableQuery, HttpPost, ODataRoute("()")]
        public IHttpActionResult Create(Role entity)
        {
            return ODataCreated(this._roleService.Create(entity), entity.Id);
        }

        // PUT: {resource}/Roles({id})
        [EnableQuery, HttpPut, ODataRoute("({id})")]
        public Role Update(int id, Role entity)
        {
            return this._roleService.Update(entity);
        }

        // PATCH: {resource}/Roles({id})
        [EnableQuery, HttpPatch, ODataRoute("({id})")]
        public Role PartialUpdate(int id, Delta<Role> entity)
        {
            return this._roleService.PartialUpdate(id, entity);
        }

        // DELETE: {resource}/Roles({id})
        [EnableQuery, HttpDelete, ODataRoute("({id})")]
        public void Delete(int id)
        {
            this.ParseResource();
            this._roleService.Delete(id);
            this.ODataDeleted(); // Set response headers
        }

        // PUT: {resource}/Roles({roleId})/Permissions({permissionId})
        [EnableQuery, HttpPut, ODataRoute("({roleId})/Permissions({permissionId})")]
        public IHttpActionResult AddPermission(int roleId, int permissionId)
        {
            return ODataCreated(this._roleService.AddPermission(roleId, permissionId), roleId);
        }

        // DELETE: {resource}/Roles({roleId})/Permissions({permissionId})
        [EnableQuery, HttpDelete, ODataRoute("({roleId})/Permissions({permissionId})")]
        public IHttpActionResult RemoveRole(int roleId, int permissionId)
        {
            return ODataCreated(this._roleService.RemovePermission(roleId, permissionId), roleId);
        }
    }
}