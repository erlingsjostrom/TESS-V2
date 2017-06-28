using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using TestRestfulAPI.Models;

namespace TestRestfulAPI.Controllers
{
    [RoutePrefix("api/v1")]
    public class DefaultController : ApiJsonController
    {
        [HttpGet]
        [Route("")]
        [Route("Version")]
        public IHttpActionResult Default()
        {
            var result = new Dictionary<string, string>()
            {
                {"Version:", "1.0"}
            };
            return Json(result, "version");
        }

        [HttpGet]
        [Route("users")]
        public IHttpActionResult Users()
        {
            //return Json(Models.User.all(), "users");
            return JsonAuto(Models.User.all());
        }

        [HttpGet]
        [Route("users/{id:int}")]
        public IHttpActionResult Users(int id)
        {
            //return Json(new User(id), "user");
            return JsonAuto(new User(id));
        }
    }
}
