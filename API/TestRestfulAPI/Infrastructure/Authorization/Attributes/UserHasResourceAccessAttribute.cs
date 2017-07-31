using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.ModelBinding;

namespace TestRestfulAPI.Infrastructure.Authorization.Attributes
{
    public class UserHasResourceAccessAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            ModelState resourceFromRequest;
            if (actionContext.ModelState.TryGetValue("resource", out resourceFromRequest))
            {
                var userValidator = new UserAuthorizationValidator();
                if (!userValidator.UserHasResourceAccess(new string[] { resourceFromRequest.Value.AttemptedValue }))
                {
                    throw new UserDoesNotHaveResourceAccessException(
                        "User does not have access to the resources: " + String.Join(", ", resourceFromRequest.Value.AttemptedValue));
                }
            }
        }
    }
}