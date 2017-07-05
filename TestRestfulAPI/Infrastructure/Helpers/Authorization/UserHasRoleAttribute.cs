using System;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace TestRestfulAPI.Infrastructure.Helpers.Authorization
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
            var userValidator = new UserAuthorizationValidator();
            if (!userValidator.UserHasRoles(this.Roles))
            {
                throw new UserDoesNotHaveRequiredRolesException(
                    "User does not have the roles: " + String.Join(", ", this.Roles));
            }
           
        }
    }
}