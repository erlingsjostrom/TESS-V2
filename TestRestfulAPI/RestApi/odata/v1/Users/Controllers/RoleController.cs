using System;
using System.Linq;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Routing;
using Microsoft.Web.Http;
using TestRestfulAPI.Entities.User;
using TestRestfulAPI.Infrastructure.Controllers;
using TestRestfulAPI.RestApi.odata.v1.Users.Services;

namespace TestRestfulAPI.RestApi.odata.Users.Controllers
{
    [ApiVersion("1.0")]
    [ODataRoutePrefix("Roles")]
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
        public void Deleted(int id)
        {
            this.ParseResource();
            this._roleService.Delete(id);
            this.ODataDeleted(); // Set response headers
        }
    }
}