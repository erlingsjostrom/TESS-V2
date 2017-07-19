using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.OData;
using TestRestfulAPI.Infrastructure.Contexts;
using TestRestfulAPI.Infrastructure.Repositories;
using TestRestfulAPI.RestApi.odata.v1.Users.Entities;
using TestRestfulAPI.RestApi.odata.v1.Users.Exceptions;
using ResourceContext = TestRestfulAPI.Infrastructure.Database.ResourceContext;

namespace TestRestfulAPI.RestApi.odata.v1.Users.Repositories
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
            var result = this.All().Include(u => u.Roles).FirstOrDefault(u => u.Id == id);
            if (result == null)
            {
                throw new UserDoesNotExistException("User with ID " + id + " does not exist");
            }
            return result;
        }

        public User Create(User entity)
        {
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
            var dbEntry = this.Get(entity.Id);
            
            ResourceContext.Context.Entry(dbEntry).CurrentValues.SetValues(entity);
            ResourceContext.Context.Entry(dbEntry).Property("CreatedAt").IsModified = false;
            
            dbEntry.Roles.Clear();
        
            //ResourceContext.Context.SaveChanges();

            var dbRoles = this.ResourceContext.Context.Set<Role>().ToList()
                .Where(r => entity.Roles.All(er => er.Id == r.Id));
            foreach (var role in dbRoles)
            {
                dbEntry.Roles.Add(role);
            }

            ResourceContext.Context.SaveChanges();

            return dbEntry;
        }

        public User PartialUpdate(int id, Delta<User> entity)
        {
            var dbEntry = this.Get(id);

            entity.Patch(dbEntry);
            ResourceContext.Context.Entry(dbEntry).Property("CreatedAt").IsModified = false;

            ResourceContext.Context.SaveChanges();

            return dbEntry;
        }

        public void Delete(int id)
        {
            var dbEntry = this.Get(id);

            ResourceContext.Context.Set<User>().Remove(dbEntry);
            ResourceContext.Context.SaveChanges();
        }

        public User GetByWindowsIdentityName(string windowsIdentity)
        {
            var user = this.All().Include("Roles").FirstOrDefault(u => u.WindowsUser == windowsIdentity);
            if (user == null)
            {
                throw new UserDoesNotExistException("User with windows identity " + windowsIdentity + " does not exist.");
            }

            return user;
        }

        public User AddRole(int userId, Role role)
        {
            var user = this.All().Include("Roles").FirstOrDefault(u => u.Id == userId);
            if (user.Roles.Any(r => r.Equals(role)))
            {
                throw new RoleAlreadyExistException("User with ID " + userId + " does already have the role " + role.Name + ".");
            }
            user.Roles.Add(role);
            ResourceContext.Context.SaveChanges();
            return user;
        }

        public User RemoveRole(int userId, Role role)
        {
            var user = this.All().Include("Roles").FirstOrDefault(u => u.Id == userId);
            if (!user.Roles.Any(r => r.Equals(role)))
            {
                throw new PermissionDoesNotExistException("User with ID " + userId + " does not have the role " + role.Name + ".");
            }
            user.Roles.Remove(role);
            ResourceContext.Context.SaveChanges();
            return user;
        }
    }
}