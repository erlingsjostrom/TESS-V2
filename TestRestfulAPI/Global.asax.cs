using System;
using System.Net;
using System.Web.Http;
using AutoMapper;
using TestRestfulAPI.RestApi.odata.v1.Articles.Services;
using TestRestfulAPI.RestApi.odata.v1.Contents.Services;
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
        public static TemplateService TemplateService;
        public static ResourceService ResourceService;
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

        protected void Application_BeginRequest()
        {
            if (Request.HttpMethod == "OPTIONS")
            {
                Response.StatusCode = (int)HttpStatusCode.OK;
                Response.AppendHeader("Access-Control-Allow-Origin", Request.Headers.GetValues("Origin")[0]);
                Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
                Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
                Response.AppendHeader("Access-Control-Allow-Credentials", "true");
                Response.End();
            }
        }
    }
}

