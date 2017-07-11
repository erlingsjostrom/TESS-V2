using System.Linq;
using System.Web.OData;
using TestRestfulAPI.Infrastructure.Repositories;
using TestRestfulAPI.RestApi.odata.v1.Users.Entities;
using TestRestfulAPI.RestApi.odata.v1.Users.Exceptions;
using ResourceContext = TestRestfulAPI.Infrastructure.Database.ResourceContext;

namespace TestRestfulAPI.RestApi.odata.v1.Users.Repositories
{
    public class PermissionRepository : SingleBaseRepository<Permission>, ISingleRepository<Permission, int>
    {
        public PermissionRepository(ResourceContext permissionContext) : base(permissionContext)
        {
            
        }

        public IQueryable<Permission> All()
        {
            return this.ResourceContext.Context.Set<Permission>();
        }
        public Permission Get(int id)
        {
            var result = this.All().FirstOrDefault(r => r.Id == id);
            if (result == null)
            {
                throw new PermissionDoesNotExistException("Permission with ID " + id + " does not exist.");
            }
            return result;
        }

        public Permission Create(Permission entity)
        {
            this.RefreshContext();
            var role = this.All().FirstOrDefault(r => r.Name == entity.Name);
            if (role != null)
            {
                throw new PermissionAlreadyExistException("Permission with name " + entity.Name + " does already exist.");
            }
            ResourceContext.Context.Set<Permission>().Add(entity);
            ResourceContext.Context.SaveChanges();

            return entity;
        }
        public Permission Update(Permission entity)
        {
            this.RefreshContext();
            var dbEntry = this.Get(entity.Id);

            ResourceContext.Context.Entry(dbEntry).CurrentValues.SetValues(entity);
            ResourceContext.Context.Entry(dbEntry).Property("CreatedAt").IsModified = false;

            ResourceContext.Context.SaveChanges();

            return dbEntry;
        }
        public Permission PartialUpdate(int id, Delta<Permission> entity)
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

            ResourceContext.Context.Set<Permission>().Remove(dbEntry);
            ResourceContext.Context.SaveChanges();
        }
        private void RefreshContext()
        {
            this.ResourceContext.Refresh();
        }
    }
}