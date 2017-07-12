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
    public class UserService : ISingleService<User, int>
    {
        private UserRepository _userRepository;
        private RoleRepository _roleRepository;

        public User GetByWindowsIdentityName(string windowsIdentityName)
        {
            InitRepositories();
            return this._userRepository.GetByWindowsIdentityName(windowsIdentityName);
        }

        public IQueryable<User> All()
        {
            InitRepositories();
            return this._userRepository.All();
        }

        public User Get(int id)
        {
            InitRepositories();
            return this._userRepository.Get(id);
        }

        public User Create(User user)
        {
            InitRepositories();
            return this._userRepository.Create(user);
        }

        public User Update(User user)
        {
            InitRepositories();
            return this._userRepository.Update(user);
        }

        public User PartialUpdate(int id, Delta<User> user)
        {
            InitRepositories();
            return this._userRepository.PartialUpdate(id, user);
        }

        public void Delete(int id)
        {
            InitRepositories();
            this._userRepository.Delete(id);
        }

        public User AddRole(int userId, int roleId)
        {
            InitRepositories();
            var role = this._roleRepository.Get(roleId);
            return this._userRepository.AddRole(userId, role);
        }
        public User RemoveRole(int userId, int roleId)
        {
            InitRepositories();
            var role = this._roleRepository.Get(roleId);
            return this._userRepository.RemoveRole(userId, role);
        }

        private void InitRepositories()
        {
            var userContext = DbContextFactory.Get<UserEntities>("TEST_TESS_USER");
            this._userRepository = new UserRepository(
                                        new ResourceContext(
                                            "User",
                                            userContext,
                                            typeof(UserEntities)
                                        )
                                    );
            this._roleRepository = new RoleRepository(
                                        new ResourceContext(
                                            "User",
                                            userContext,
                                            typeof(UserEntities)
                                        )
                                    );
        }
    }
}