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
    [ODataRoutePrefix("Users")]
    [UserHasRole("UserControl")]
    public class UserController : ResourceODataController, ICrudController<User>
    {
        private readonly UserService _userService = GlobalServices.UserService;

        // GET: {resource}/Users
        [EnableQuery, HttpGet, ODataRoute("()")]
        public IQueryable<User> Get()
        {
            return this._userService.All();
        }

        // GET: {resource}/Users({id})
        [EnableQuery, HttpGet, ODataRoute("({id})")]
        public User Get(int id)
        {
            return this._userService.Get(id);
        }

        // POST: {resource}/Users()
        [EnableQuery, HttpPost, ODataRoute("()")]
        public IHttpActionResult Create(User entity)
        {
            return ODataCreated(this._userService.Create(entity), entity.Id);
        }

        // PUT: {resource}/Users({id})
        [EnableQuery, HttpPut, ODataRoute("({id})")]
        public User Update(int id, User entity)
        {
            return this._userService.Update(entity);
        }

        // PATCH: {resource}/Users({id})
        [EnableQuery, HttpPatch, ODataRoute("({id})")]
        public User PartialUpdate(int id, Delta<User> entity)
        {
            return this._userService.PartialUpdate(id, entity);
        }

        // DELETE: {resource}/Users({id})
        [EnableQuery, HttpDelete, ODataRoute("({id})")]
        public void Delete(int id)
        {
            this.ParseResource();
            this._userService.Delete(id);
            this.ODataDeleted(); // Set response headers
        }
    }
}
