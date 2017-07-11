using System.Web.Http;
using TestRestfulAPI.Infrastructure.Contexts;
using TestRestfulAPI.Infrastructure.Database;
using TestRestfulAPI.RestApi.odata.v1.Articles.Services;
using TestRestfulAPI.RestApi.odata.v1.Customers.Services;
using TestRestfulAPI.RestApi.odata.v1.Users.Repositories;
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
            GlobalServices.UserService = new UserService(
                new UserRepository(
                    new ResourceContext(
                        "User", 
                        DbContextFactory.Get<UserEntities>("TEST_TESS_USER"),
                        typeof(UserEntities)
                    )
                )
            );
            GlobalServices.RoleService = new RoleService(
                new RoleRepository(
                    new ResourceContext(
                        "Role",
                        DbContextFactory.Get<UserEntities>("TEST_TESS_USER"),
                        typeof(UserEntities)
                    )
                )
            );
            GlobalServices.PermissionService = new PermissionService(
                new PermissionRepository(
                    new ResourceContext(
                        "Permission",
                        DbContextFactory.Get<UserEntities>("TEST_TESS_USER"),
                        typeof(UserEntities)
                    )
                )
            );
            GlobalServices.ArticleService = new ArticleService(GlobalServices.UserService);
            GlobalServices.CustomerService = new CustomerService(GlobalServices.UserService);
        }
    }
}