using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.OData;
using TestRestfulAPI.Infrastructure.Exceptions;
using TestRestfulAPI.Infrastructure.Repositories;
using TestRestfulAPI.RestApi.odata.v1.Offers.Entities;
using TestRestfulAPI.RestApi.odata.v1.Offers.Exceptions;
using ResourceContext = TestRestfulAPI.Infrastructure.Database.ResourceContext;

namespace TestRestfulAPI.RestApi.odata.v1.Offers.Repositories
{
    public class TextItemRepository : BaseRepository<TextItem>, IRepository<TextItem, int, string>
    {
        public TextItemRepository(IEnumerable<ResourceContext> resourceContexts) : base(resourceContexts)
        {
        }

        public IEnumerable<IQueryable<TextItem>> All()
        {
            var results = new List<IQueryable<TextItem>>();
            foreach (var resourceContext in this.ResourceContexts)
            {
                results.Add(resourceContext.Context.Set<TextItem>());
            }
            return results;
        }
        public ResultSet<IQueryable<TextItem>> AllWithResourceContext()
        {
            var results = new ResultSet<IQueryable<TextItem>>("TextItems");
            foreach (var resourceContext in this.ResourceContexts)
            {
                results.Add(resourceContext.Name, resourceContext.Context.Set<TextItem>());
            }
            return results;
        }
        public IQueryable<TextItem> All(string resource)
        {
            var results = this.GetAndValidateResource(resource);
            return results.Context.Set<TextItem>();
        } 

        public TextItem Get(string resource, int id)
        {
            var results = GetAndValidateResource(resource);
            var textitem = results
                .Context.Set<TextItem>()
                .FirstOrDefault(o => o.Id == id);
            if (textitem == null)
            {
                throw new TextItemDoesNotExistException("TextItem with ID " + id + " does not exist");
            }

            return textitem;
        }

        public TextItem Create(string resource, TextItem entity)
        {
            var results = GetAndValidateResource(resource);

            results.Context.Set<TextItem>().Add(entity);
            this.SetTimeStamps(ref entity);
            results.Context.SaveChanges();

            return entity;
        }

        public TextItem Update(string resource, TextItem entity)
        {
            var results = GetAndValidateResource(resource);

            var dbEntry = this.Get(resource, entity.Id);

            results.Context.Entry(dbEntry).CurrentValues.SetValues(entity);
            results.Context.Entry(dbEntry).Property("CreatedAt").IsModified = false;

            results.Context.SaveChanges();
            return dbEntry;
        }

        public TextItem PartialUpdate(string resource, int id, Delta<TextItem> entity)
        {
            var results = GetAndValidateResource(resource);
            var dbEntry = this.Get(resource, id);
            entity.Patch(dbEntry);
            results.Context.Entry(dbEntry).Property("CreatedAt").IsModified = false;

            results.Context.SaveChanges();

            return dbEntry;
        }

        public void Delete(string resource, int id)
        {
            var results = GetAndValidateResource(resource);

            var dbEntry = this.Get(resource, id);

            results.Context.Set<TextItem>().Remove(dbEntry);
            results.Context.SaveChanges();
        }
        private ResourceContext GetAndValidateResource(string resource)
        {
            var results = this.ResourceContexts.FirstOrDefault(c => c.Name == resource);
            if (results == null)
            {
                throw new InvalidDbContextTypeException("Resource not found");
            }
            return results;
        }
        private void SetTimeStamps(ref TextItem entity)
        {
            if (entity.CreatedAt == new DateTime())
            {
                entity.CreatedAt = DateTime.Now;
            }
            entity.UpdatedAt = DateTime.Now;
        }
    }
}