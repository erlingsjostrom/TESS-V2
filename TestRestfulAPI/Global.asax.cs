using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using System.Web.UI;
using TestRestfulAPI.Entities.User;
using TestRestfulAPI.Infrastructure.Database;
using TestRestfulAPI.Infrastructure.Helpers;
using TestRestfulAPI.Infrastructure.Repositories;
using TestRestfulAPI.RestApi.v1.Users.Repositories;
using TestRestfulAPI.RestApi.v1.Users.Services;


namespace TestRestfulAPI
{
    public static class GlobalServices
    {
        public static UserService UserService = new UserService(
            new UserRepository(
                new ResourceContext("User", new UserEntities(), typeof(UserEntities))
            )
       );
    }

    public static class GlobalVariables
    {

        #if DEBUG
                public static readonly bool IsDebuggingEnabled = true;
        #else
                public static readonly bool IsDebuggingEnabled = false;
        #endif

    }

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Filters.Add(new Infrastructure.Filters.ExceptionFilterAttribute());
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }
    }
}

