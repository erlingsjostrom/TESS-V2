using System;
using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.Http;
using System.Web.OData.Extensions;
using TestRestfulAPI.Entities.TESS;

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
            
            config.AddApiVersioning(o => o.AssumeDefaultVersionWhenUnspecified = true);
            

            ODataModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Article>("Articles");
            builder.EntityType<Article>().OrderBy().Filter().Select();

            config.MapODataServiceRoute(
                routeName: "Articles",
                routePrefix: "odata/{apiVersion}/{resource}/",
                model: builder.GetEdmModel());

        }
    }
}
