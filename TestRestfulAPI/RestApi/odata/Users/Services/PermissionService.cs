using System.Linq;
using System.Web.OData;
using TestRestfulAPI.Entities.User;
using TestRestfulAPI.Infrastructure.Database;
using TestRestfulAPI.RestApi.odata.Users.Repositories;
using ResourceContext = TestRestfulAPI.Infrastructure.Database.ResourceContext;

namespace TestRestfulAPI.RestApi.odata.Users.Services
{
    public class PermissionService
    {
        private PermissionRepository _permissionRepository;

        public PermissionService(PermissionRepository permissionRepository)
        {
            this._permissionRepository = permissionRepository;
        }

        public IQueryable<Permission> All()
        {
            return this._permissionRepository.All();
        }

        public Permission Get(int id)
        {
            return this._permissionRepository.Get(id);
        }

        public Permission Create(Permission permission)
        {
            return this._permissionRepository.Create(permission);
        }
        public Permission Update(Permission permission)
        {
            return this._permissionRepository.Update(permission);
        }
        public Permission PartialUpdate(int id, Delta<Permission> permission)
        {
            return this._permissionRepository.PartialUpdate(id, permission);
        }
        public void Delete(int id)
        {
            this._permissionRepository.Delete(id);
        }

        private void InitRepository()
        {
            this._permissionRepository = new PermissionRepository(
                new ResourceContext(
                    "Permission",
                    DbContextFactory.Get<UserEntities>("TEST_TESS_USER"),
                    typeof(UserEntities)
                )
            );
        }
    }
}