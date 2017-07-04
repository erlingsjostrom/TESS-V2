using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using System.Web.UI;
using TestRestfulAPI.Entities.User;
using TestRestfulAPI.Infrastructure.Helpers;
using TestRestfulAPI.Infrastructure.Repositories;
using TestRestfulAPI.RestApi.v1.Users.Repositories;


namespace TestRestfulAPI
{

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Filters.Add(new Infrastructure.Filters.NotImplExceptionFilterAttribute());
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            var user = HttpContext.Current.User;
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            var user = HttpContext.Current.User;
            if (user != null)
            {
                var userRepository = new UserRepository(new ResourceContext("User", new UserEntities()));
                var dbUser = userRepository.GetByWindowsIdentityName(user.Identity.Name);
                
                //HttpContext.Current.Session.Add("__User", user.Identity.Name);

            }
        }
    }
}

