using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using Microsoft.OData.Edm;
using TestRestfulAPI.RestApi.odata.v1.Articles.Entities;
using TestRestfulAPI.RestApi.odata.v1.Contents.Entities;
using TestRestfulAPI.RestApi.odata.v1.Customers.Entities;
using TestRestfulAPI.RestApi.odata.v1.Offers.Entities;
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
            config.EnableCors();
            config.Select().Expand().Filter().OrderBy().MaxTop(600).Count();
            config.MapODataServiceRoute(
                routeName: "odata",
                routePrefix: "odata/{apiVersion}/{resource}/",
                model: GetEdmModel());
            var corsAttr = new EnableCorsAttribute("http://localhost:4200,http://10.41.142.153:4200", "*", "*");
            corsAttr.SupportsCredentials = true;
            config.EnableCors(corsAttr);

        }

        private static IEdmModel GetEdmModel()
        {
            ODataModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Article>("Articles").EntityType.Name = "Article";
            builder.EntityType<Article>().OrderBy().Filter().Select().Expand();

            builder.EntitySet<Offer>("Offers");
            builder.EntityType<Offer>().OrderBy().Filter().Select().Expand();

            builder.EntitySet<Customer>("Customers").EntityType.Name = "Customer";
            builder.EntityType<Customer>().OrderBy().Filter().Select().Expand();

            builder.EntitySet<User>("Users").EntityType.Name = "User";
            builder.EntityType<User>().OrderBy().Filter().Select().Expand();

            builder.EntitySet<Role>("Roles").EntityType.Name = "Role";
            builder.EntityType<Role>().OrderBy().Filter().Select().Expand();

            builder.EntitySet<Permission>("Permissions").EntityType.Name = "Permission";
            builder.EntityType<Permission>().OrderBy().Filter().Select().Expand();

            builder.EntitySet<Content>("Contents").EntityType.Name = "Content";
            builder.EntityType<Content>().OrderBy().Filter().Select().Expand();

            builder.EntitySet<TextItem>("TextItems").EntityType.Name = "TextItem";
            builder.EntityType<TextItem>().OrderBy().Filter().Select().Expand();

            builder.EntitySet<Template>("Templates").EntityType.Name = "Template";
            builder.EntityType<Template>().OrderBy().Filter().Select().Expand();

            builder.EntitySet<Resource>("Resources").EntityType.Name = "Resource";
            builder.EntityType<Resource>().OrderBy().Filter().Select().Expand();

            var edmModel = builder.GetEdmModel();
            return edmModel;
        }
    }
}
