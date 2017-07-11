using System.Linq;
using System.Web.OData;
using TestRestfulAPI.Infrastructure.Contexts;
using TestRestfulAPI.Infrastructure.Database;
using TestRestfulAPI.RestApi.odata.v1.Users.Entities;
using TestRestfulAPI.RestApi.odata.v1.Users.Repositories;
using ResourceContext = TestRestfulAPI.Infrastructure.Database.ResourceContext;

namespace TestRestfulAPI.RestApi.odata.v1.Users.Services
{
    public class RoleService
    {
        private RoleRepository _roleRepository;

        public RoleService(RoleRepository roleRepository)
        {
            this._roleRepository = roleRepository;
        }

        public IQueryable<Role> All()
        {
            return this._roleRepository.All();
        }

        public Role Get(int id)
        {
            return this._roleRepository.Get(id);
        }

        public Role Create(Role role)
        {
            return this._roleRepository.Create(role);
        }
        public Role Update(Role role)
        {
            return this._roleRepository.Update(role);
        }
        public Role PartialUpdate(int id, Delta<Role> role)
        {
            return this._roleRepository.PartialUpdate(id, role);
        }
        public void Delete(int id)
        {
            this._roleRepository.Delete(id);
        }

        private void InitRepository()
        {
            this._roleRepository = new RoleRepository(
                new ResourceContext(
                    "Role",
                    DbContextFactory.Get<UserEntities>("TEST_TESS_USER"),
                    typeof(UserEntities)
                )
            );
        }
    }
}