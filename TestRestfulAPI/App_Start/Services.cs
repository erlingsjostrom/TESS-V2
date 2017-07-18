using System.Web.Http;
using TestRestfulAPI.RestApi.odata.v1.Articles.Services;
using TestRestfulAPI.RestApi.odata.v1.Contents.Services;
using TestRestfulAPI.RestApi.odata.v1.Customers.Services;
using TestRestfulAPI.RestApi.odata.v1.Offers.Services;
using TestRestfulAPI.RestApi.odata.v1.Users.Services;

namespace TestRestfulAPI
{
    public static class Services
    {
        /// <summary>
        /// Register an instance for all Services 
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            GlobalServices.UserService = new UserService();
            GlobalServices.RoleService = new RoleService();
            GlobalServices.PermissionService = new PermissionService();
            GlobalServices.ArticleService = new ArticleService(GlobalServices.UserService);
            GlobalServices.CustomerService = new CustomerService(GlobalServices.UserService);
            GlobalServices.OfferService = new OfferService(GlobalServices.UserService);
            GlobalServices.ContentService = new ContentService(GlobalServices.UserService);
            GlobalServices.TextItemService = new TextItemService(GlobalServices.UserService);
            GlobalServices.TemplateService = new TemplateService(GlobalServices.UserService);
        }
    }
}