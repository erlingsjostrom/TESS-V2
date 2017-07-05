using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace TestRestfulAPI.Infrastructure.Authorization.Attributes
{
    public class UserHasPermissionAttribute : ActionFilterAttribute
    {
        public string[] Permissions { get; set; }
        public UserHasPermissionAttribute(params string[] values)
        {
            this.Permissions = values;
        }
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var userValidator = new UserAuthorizationValidator();
            if (!userValidator.UserHasPermission(this.Permissions))
            {
                throw new UserDoesNotHaveRequiredRolesException(
                    "User does not have the required permissions: " + String.Join(", ", this.Permissions));
            }

        }
    }

}