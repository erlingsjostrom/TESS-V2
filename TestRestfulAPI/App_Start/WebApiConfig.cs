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
            builder.EntityType<Article>().OrderBy().Filter().Select().Expand();
            builder.EntitySet<Customer>("Customers");
            builder.EntityType<Customer>().OrderBy().Filter().Select().Expand();
            builder.EntitySet<User>("Users");
            builder.EntityType<User>().OrderBy().Filter().Select().Expand();
            builder.EntitySet<Role>("Roles");
            builder.EntityType<Role>().OrderBy().Filter().Select().Expand();
            builder.EntitySet<Permission>("Permissions");
            builder.EntityType<Permission>().OrderBy().Filter().Select().Expand();

            var edmModel = builder.GetEdmModel();
            return edmModel;
        }
    }
}
