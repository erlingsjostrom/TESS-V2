﻿using System.IO;
using System.Web;
using System.Web.Http;
using System.Collections.Generic;
using TestRestfulAPI.Entities.User;
using TestRestfulAPI.Infrastructure.Controllers;
using TestRestfulAPI.Infrastructure.Database;
using TestRestfulAPI.Infrastructure.Repositories;
using TestRestfulAPI.RestApi.v1.Users.Repositories;
using TestRestfulAPI.RestApi.v1.Users.Services;

namespace TestRestfulAPI.RestApi.v1.Users.Controllers
{
    [RoutePrefix("api/v1/users") /*Route("{action=Users}")*/]
    public class UserController : ApiJsonController
    {
        private readonly UserService _userService = GlobalServices.UserService;

        [HttpGet, Route("")]
        public IHttpActionResult Users()
        {
            return Json(this._userService.All(), "Users");
        }

        [HttpGet, Route("{id:int}")]
        public IHttpActionResult Users(int id)
        {
            return Json(this._userService.Get(id), "User");
        }

        [HttpPost, Route("")]
        public IHttpActionResult Users(User user)
        {
            var result = this._userService.Create(user);
            return JsonCreated(result, result.Id);
        }

        [HttpPut, Route("{id:int}")]
        public IHttpActionResult Users(int id, User user)
        {
            var updatedArticle = this._userService.Update(user);
            return Json(updatedArticle);
        }
    }
}
