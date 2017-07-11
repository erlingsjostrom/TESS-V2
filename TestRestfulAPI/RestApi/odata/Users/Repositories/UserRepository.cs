using System;
using System.Data.Entity;
using System.Linq;
using System.Web.OData;
using TestRestfulAPI.Entities.User;
using TestRestfulAPI.Infrastructure.Repositories;
using TestRestfulAPI.RestApi.odata.Users.Exceptions;
using ResourceContext = TestRestfulAPI.Infrastructure.Database.ResourceContext;

namespace TestRestfulAPI.RestApi.odata.Users.Repositories
{
    public class UserRepository : SingleBaseRepository<User>, ISingleRepository<User, int>
    {
        public UserRepository(ResourceContext userContext) : base(userContext)
        {}

        public IQueryable<User> All()
        {
            return this.ResourceContext.Context.Set<User>();
        }
        public User Get(int id)
        {
            var result = this.All().FirstOrDefault(u => u.Id == id);
            if (result == null)
            {
                throw new UserDoesNotExistException("User with ID " + id + " does not exist");
            }
            return result;
        }

        public User Create(User entity)
        {
            this.RefreshContext();
            var user = this.All().FirstOrDefault(u => u.WindowsUser == entity.WindowsUser);
            if (user != null)
            {
                throw new UserAlreadyExistException("User with Windows identity " + entity.WindowsUser + " does already exist.");
            }
            ResourceContext.Context.Set<User>().Add(entity);
            ResourceContext.Context.SaveChanges();
           
            return entity;
        }

        public User Update(User entity)
        {
            this.RefreshContext();
            var dbEntry = this.Get(entity.Id);

            ResourceContext.Context.Entry(dbEntry).CurrentValues.SetValues(entity);
            ResourceContext.Context.Entry(dbEntry).Property("CreatedAt").IsModified = false;

            ResourceContext.Context.SaveChanges();

            return dbEntry;
        }

        public User PartialUpdate(int id, Delta<User> entity)
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

            ResourceContext.Context.Set<User>().Remove(dbEntry);
            ResourceContext.Context.SaveChanges();
        }

        public User GetByWindowsIdentityName(string windowsIdentity)
        {
            this.RefreshContext();
            var user = this.All().Include("Roles").FirstOrDefault(u => u.WindowsUser == windowsIdentity);
            if (user == null)
            {
                throw new UserDoesNotExistException("User with windows identity " + windowsIdentity + " does not exist.");
            }

            return user;
        }

        private void RefreshContext()
        {
            this.ResourceContext.Refresh();
        }
    }
}