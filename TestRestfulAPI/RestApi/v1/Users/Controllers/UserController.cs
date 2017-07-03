using System.IO;
using System.Web;
using System.Web.Http;
using System.Collections.Generic;
using TestRestfulAPI.Infrastructure.Controllers;
using TestRestfulAPI.Infrastructure.Repositories;
using TestRestfulAPI.RestApi.v1.Users.Context;
using TestRestfulAPI.RestApi.v1.Users.Repositories;

namespace TestRestfulAPI.RestApi.v1.Users.Controllers
{
    [RoutePrefix("api/v1/users") /*Route("{action=Users}")*/]
    public class UserController : ApiJsonController
    {
        private readonly UserRepository userRepository;

        public UserController()
        {
            this.userRepository = new UserRepository(
                new ResourceContext("UserDB1", new UserEntities())
            );
        }
        [HttpGet, Route("")]
        public IHttpActionResult Users()
        {
            return Json(this.userRepository.All(), "Users");
        }

        [HttpGet, Route("{id:int}")]
        public IHttpActionResult Users(int id)
        {
            return Json(this.userRepository.Get(id), "User");
        }

        [HttpPost, Route("")]
        public IHttpActionResult Users(User user)
        {
            var result = this.userRepository.Create(user);
            return JsonCreated(result, result.Id);
        }
    }
}
