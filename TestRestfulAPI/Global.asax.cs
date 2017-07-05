using System;
using System.Web.Http;
using TestRestfulAPI.RestApi.v1.Articles.Services;
using TestRestfulAPI.RestApi.v1.Users.Services;


namespace TestRestfulAPI
{
    public static class GlobalServices
    {
        public static UserService UserService;
        public static ArticleService ArticleService;
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
            GlobalConfiguration.Configure(Services.Register);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }
    }
}

