using System.Linq;
using System.Web.OData;
using TestRestfulAPI.Infrastructure.Repositories;
using TestRestfulAPI.RestApi.odata.v1.Users.Entities;
using TestRestfulAPI.RestApi.odata.v1.Users.Exceptions;
using ResourceContext = TestRestfulAPI.Infrastructure.Database.ResourceContext;

namespace TestRestfulAPI.RestApi.odata.v1.Users.Repositories
{
    public class RoleRepository : SingleBaseRepository<Role>, ISingleRepository<Role, int>
    {
        public RoleRepository(ResourceContext roleContext) : base(roleContext)
        {
        }

        public IQueryable<Role> All()
        {
            return this.ResourceContext.Context.Set<Role>();
        }

        public Role Get(int id)
        {
            var result = this.All().FirstOrDefault(r => r.Id == id);
            if (result == null)
            {
                throw new RoleDoesNotExistException("Role with ID " + id + " does not exist.");
            }
            return result;
        }

        public Role Create(Role entity)
        {
            this.RefreshContext();
            var role = this.All().FirstOrDefault(r => r.Name == entity.Name);
            if (role != null)
            {
                throw new RoleAlreadyExistException("Role with name " + entity.Name + " does already exist.");
            }
            ResourceContext.Context.Set<Role>().Add(entity);
            ResourceContext.Context.SaveChanges();

            return entity;

        }

        public Role Update(Role entity)
        {
            this.RefreshContext();
            var dbEntry = this.Get(entity.Id);

            ResourceContext.Context.Entry(dbEntry).CurrentValues.SetValues(entity);
            ResourceContext.Context.Entry(dbEntry).Property("CreatedAt").IsModified = false;

            ResourceContext.Context.SaveChanges();

            return dbEntry;
        }

        public Role PartialUpdate(int id, Delta<Role> entity)
        {
            this.RefreshContext();
            var dbEntry = this.Get(id);

            entity.Patch(dbEntry);
            ResourceContext.Context.Entry(dbEntry).Property("CreatedAt").IsModified = false;

            ResourceContext.Context.SaveChanges();

            return dbEntry;
        }

        public void Delete(int id)
        {
            this.RefreshContext();
            var dbEntry = this.Get(id);

            ResourceContext.Context.Set<Role>().Remove(dbEntry);
            ResourceContext.Context.SaveChanges();
        }

        private void RefreshContext()
        {
            this.ResourceContext.Refresh();
        }
    }
}