using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using Microsoft.OData.Edm;
using TestRestfulAPI.RestApi.odata.v1.Articles.Entities;
using TestRestfulAPI.RestApi.odata.v1.Customers.Entities;
using TestRestfulAPI.RestApi.odata.v1.Users.Entities;

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
          
            config.MapODataServiceRoute(
                routeName: "odata",
                routePrefix: "odata/{apiVersion}/{resource}/",
                model: GetEdmModel());

        }

        private static IEdmModel GetEdmModel()
        {
            ODataModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Article>("Articles");
            builder.EntityType<Article>().OrderBy().Filter().Select();
            builder.EntitySet<Customer>("Customers");
            builder.EntityType<Customer>().OrderBy().Filter().Select();
            builder.EntitySet<User>("Users");
            builder.EntityType<User>().OrderBy().Filter().Select();

            var edmModel = builder.GetEdmModel();
            return edmModel;
        }
    }
}
