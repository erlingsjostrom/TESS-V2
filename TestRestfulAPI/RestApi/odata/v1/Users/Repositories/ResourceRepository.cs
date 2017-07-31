using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.OData;
using TestRestfulAPI.Infrastructure.Repositories;
using TestRestfulAPI.RestApi.odata.v1.Users.Entities;
using TestRestfulAPI.RestApi.odata.v1.Users.Exceptions;
using ResourceContext = TestRestfulAPI.Infrastructure.Database.ResourceContext;

namespace TestRestfulAPI.RestApi.odata.v1.Users.Repositories
{
    public class ResourceRepository : SingleBaseRepository<Resource>, ISingleRepository<Resource, int>
    {
        public ResourceRepository(ResourceContext resourceContext) : base(resourceContext)
        {
        }

        public IQueryable<Resource> All()
        {
            return this.ResourceContext.Context.Set<Resource>();
        }

        public Resource Get(int id)
        {
            var result = this.All().FirstOrDefault(r => r.Id == id);
            if (result == null)
            {
                throw new ResourceDoesNotExistException("Resource with ID " + id + " does not exist.");
            }
            return result;
        }

        public Resource Create(Resource entity)
        {
            var role = this.All().FirstOrDefault(r => r.Name == entity.Name);
            if (role != null)
            {
                throw new ResourceAlreadyExistException("Resource with name " + entity.Name + " does already exist.");
            }
            ResourceContext.Context.Set<Resource>().Add(entity);
            ResourceContext.Context.SaveChanges();

            return entity;
        }

        public Resource Update(Resource entity)
        {
            var dbEntry = this.Get(entity.Id);

            ResourceContext.Context.Entry(dbEntry).CurrentValues.SetValues(entity);
            ResourceContext.Context.Entry(dbEntry).Property("CreatedAt").IsModified = false;

            ResourceContext.Context.SaveChanges();

            return dbEntry;
        }

        public Resource PartialUpdate(int id, Delta<Resource> entity)
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

            ResourceContext.Context.Set<Resource>().Remove(dbEntry);
            ResourceContext.Context.SaveChanges();
        }
    }
}