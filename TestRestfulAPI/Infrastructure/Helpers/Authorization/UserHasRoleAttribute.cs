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
            var userName = HttpContext.Current.User.Identity.Name;
            var dbUser = GlobalServices.UserService.GetByWindowsIdentityName(userName);
            var userValidator = new UserAuthorizationValidator();
            if (!userValidator.UserHasRoles(dbUser, this.Roles))
            {
                throw new UnauthorizedAccessException("User does not have acceess.");
            }
        }
    }
}