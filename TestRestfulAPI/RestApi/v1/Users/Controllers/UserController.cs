using System.IO;
using System.Web;
using System.Web.Http;
using System.Collections.Generic;
using TestRestfulAPI.Entities.User;
using TestRestfulAPI.Infrastructure.Controllers;
using TestRestfulAPI.Infrastructure.Repositories;
using TestRestfulAPI.RestApi.v1.Users.Repositories;

namespace TestRestfulAPI.RestApi.v1.Users.Controllers
{
    [RoutePrefix("api/v1/users") /*Route("{action=Users}")*/]
    public class UserController : ApiJsonController
    {
        private readonly UserRepository _userRepository;

        public UserController()
        {
            this._userRepository = new UserRepository(
                new ResourceContext("UserDB1", new UserEntities())
            );
        }
        [HttpGet, Route("")]
        public IHttpActionResult Users()
        {
            return Json(this._userRepository.All(), "Users");
        }

        [HttpGet, Route("{id:int}")]
        public IHttpActionResult Users(int id)
        {
            return Json(this._userRepository.Get(id), "User");
        }

        [HttpPost, Route("")]
        public IHttpActionResult Users(User user)
        {
            var result = this._userRepository.Create(user);
            return JsonCreated(result, result.Id);
        }

        [HttpPut, Route("update")]
        public IHttpActionResult Update(User user)
        {
            var result = this._userRepository.Update(user);
            return JsonCreated(result, result.Id);
        }
    }
}
