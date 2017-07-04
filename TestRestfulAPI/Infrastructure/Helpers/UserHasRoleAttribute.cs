using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using TestRestfulAPI.RestApi.v1.Users.Services;

namespace TestRestfulAPI.Infrastructure.Helpers
{
    public class UserHasRoleAttribute : ActionFilterAttribute
    {
        public string[] Roles { get; set; }

        public UserHasRoleAttribute(params string[] values)
        {
            this.Roles = values;
        }
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var test = String.Join(" / ", Roles);
        }
    }
}