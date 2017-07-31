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
    public class ResourceService : ISingleService<Resource, int>
    {
        private ResourceRepository _resourceRepository;
        public IQueryable<Resource> All()
        {
            InitRepository();
            return this._resourceRepository.All();
        }

        public Resource Get(int id)
        {
            InitRepository();
            return this._resourceRepository.Get(id);
        }

        public Resource Create(Resource resource)
        {
            InitRepository();
            return this._resourceRepository.Create(resource);
        }
        public Resource Update(Resource resource)
        {
            InitRepository();
            return this._resourceRepository.Update(resource);
        }
        public Resource PartialUpdate(int id, Delta<Resource> resource)
        {
            InitRepository();
            return this._resourceRepository.PartialUpdate(id, resource);
        }
        public void Delete(int id)
        {
            InitRepository();
            this._resourceRepository.Delete(id);
        }
        private void InitRepository()
        {
            var userContext = DbContextFactory.Get<UserEntities>("TEST_TESS_USER");
            this._resourceRepository = new ResourceRepository(
                new ResourceContext(
                    "Resource",
                    userContext,
                    typeof(UserEntities)
                )
            );
        }
    }
}