using System.Linq;
using System.Web.OData;
using TestRestfulAPI.Infrastructure.Contexts;
using TestRestfulAPI.Infrastructure.Database;
using TestRestfulAPI.Infrastructure.Services;
using TestRestfulAPI.RestApi.odata.v1.Users.Entities;
using TestRestfulAPI.RestApi.odata.v1.Users.Repositories;
using ResourceContext = TestRestfulAPI.Infrastructure.Database.ResourceContext;

namespace TestRestfulAPI.RestApi.odata.v1.Users.Services
{
    public class RoleService : ISingleService<Role, int>
    {
        private RoleRepository _roleRepository;
        private PermissionRepository _permissionRepository;

        public IQueryable<Role> All()
        {
            InitRepository();
            return this._roleRepository.All();
        }

        public Role Get(int id)
        {
            InitRepository();
            return this._roleRepository.Get(id);
        }

        public Role Create(Role role)
        {
            InitRepository();
            return this._roleRepository.Create(role);
        }
        public Role Update(Role role)
        {
            InitRepository();
            return this._roleRepository.Update(role);
        }
        public Role PartialUpdate(int id, Delta<Role> role)
        {
            InitRepository();
            return this._roleRepository.PartialUpdate(id, role);
        }
        public void Delete(int id)
        {
            InitRepository();
            this._roleRepository.Delete(id);
        }
        //public Role AddPermission(int roleId, int permissionId)
        //{
        //    InitRepository();
        //    var permission = this._permissionRepository.Get(permissionId);
        //    return this._roleRepository.AddPermission(roleId, permission);
        //}
        //public Role RemovePermission(int roleId, int permissionId)
        //{
        //    InitRepository();
        //    var permission = this._permissionRepository.Get(permissionId);
        //    return this._roleRepository.RemovePermission(roleId, permission);
        //}

        private void InitRepository()
        {
            var userContext = DbContextFactory.Get<UserEntities>("TEST_TESS_USER");
            this._roleRepository = new RoleRepository(
                                            new ResourceContext(
                                                "Role",
                                                userContext,
                                                typeof(UserEntities)
                                            )
                                        );
            this._permissionRepository = new PermissionRepository(
                                            new ResourceContext(
                                                "Permission",
                                                userContext,
                                                typeof(UserEntities)
                                            )
                                        );
        }

    }
}