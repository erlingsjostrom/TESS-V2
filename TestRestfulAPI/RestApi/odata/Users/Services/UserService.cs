using System.Linq;
using System.Web.OData;
using TestRestfulAPI.Entities.User;
using TestRestfulAPI.Infrastructure.Database;
using TestRestfulAPI.RestApi.odata.Users.Repositories;
using ResourceContext = TestRestfulAPI.Infrastructure.Database.ResourceContext;

namespace TestRestfulAPI.RestApi.odata.Users.Services
{
    public class UserService
    {
        private UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public User GetByWindowsIdentityName(string windowsIdentityName)
        {
            return this._userRepository.GetByWindowsIdentityName(windowsIdentityName);
        }

        public IQueryable<User> All()
        {
            return this._userRepository.All();
        }

        public User Get(int id)
        {
            return this._userRepository.Get(id);
        }

        public User Create(User user)
        {
            return this._userRepository.Create(user);
        }

        public User Update(User user)
        {
            return this._userRepository.Update(user);
        }

        public User PartialUpdate(int id, Delta<User> user)
        {
            return this._userRepository.PartialUpdate(id, user);
        }

        public void Delete(int id)
        {
            this._userRepository.Delete(id);
        }

        private void InitRepository()
        {
            this._userRepository = new UserRepository(
                new ResourceContext(
                    "User",
                    DbContextFactory.Get<UserEntities>("TEST_TESS_USER"),
                    typeof(UserEntities)
                )
            );
        }
    }
}