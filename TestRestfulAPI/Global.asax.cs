using System;
using System.Web.Http;
using AutoMapper;
using TestRestfulAPI.RestApi.odata.v1.Articles.Services;
using TestRestfulAPI.RestApi.odata.v1.Customers.Services;
using TestRestfulAPI.RestApi.odata.v1.Offers.Services;
using TestRestfulAPI.RestApi.odata.v1.Users.Services;

namespace TestRestfulAPI
{
    public static class GlobalServices
    {
        public static UserService UserService;
        public static ArticleService ArticleService;
        public static CustomerService CustomerService;
        public static RoleService RoleService;
        public static PermissionService PermissionService;
        public static OfferService OfferService;
        public static ContentService ContentService;
        public static TextItemService TextItemService;
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
            GlobalConfiguration.Configure(EntityMappings.Register);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }
    }
}

