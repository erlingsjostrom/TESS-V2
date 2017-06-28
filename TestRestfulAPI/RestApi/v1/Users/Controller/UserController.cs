using System.Web.Http;
using TestRestfulAPI.Infrastructure.Controllers;
using TestRestfulAPI.RestApi.v1.Users.Models;

namespace TestRestfulAPI.RestApi.v1.Users.Controller
{
    [RoutePrefix("api/v1/users") /*Route("{action=Users}")*/]
    public class UserController : ApiJsonController
    {
        [HttpGet, Route("")]
        public IHttpActionResult Users()
        {
            //return Json(Models.User.all(), "users");
            return JsonAuto(Models.User.all());
        }

        [HttpGet, Route("{id:int}")]
        public IHttpActionResult Users(int id)
        {
            //return Json(new User(id), "user");
            return JsonAuto(new User(id));
        }
    }
}
