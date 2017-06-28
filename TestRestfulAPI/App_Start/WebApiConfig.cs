using System.Web.Http;

namespace TestRestfulAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();
            //config.Routes.MapHttpRoute(
            //        name: "default",
            //        routeTemplate: "api/{version}/{controller}/{action}/{param}",
            //        defaults: new { version = "v1", controller = "Default", action = "version", param = RouteParameter.Optional }
            //);
        }
    }
}
