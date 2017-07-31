using TestRestfulAPI.Infrastructure.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestRestfulAPI.RestApi.odata.v1.Contents.Entities;
using System.Web.OData;
using TestRestfulAPI.Infrastructure.Exceptions;
using TestRestfulAPI.RestApi.odata.v1.Contents.Exceptions;
using ResourceContext = TestRestfulAPI.Infrastructure.Database.ResourceContext;

namespace TestRestfulAPI.RestApi.odata.v1.Contents.Repositories
{
    public class TemplateRepository : BaseRepository<Template>, IRepository<Template, int, string>
    {
        private ContentRepository _contentRepository;

        public TemplateRepository(IEnumerable<ResourceContext> resourceContexts) : base(resourceContexts)
        {
        }
        public IEnumerable<IQueryable<Template>> All()
        {
            var results = new List<IQueryable<Template>>();
            foreach (var resourceContext in this.ResourceContexts)
            {
                results.Add(resourceContext.Context.Set<Template>());
            }
            return results;
        }
        public ResultSet<IQueryable<Template>> AllWithResourceContext()
        {
            var results = new ResultSet<IQueryable<Template>>("Templates");
            foreach (var resourceContext in this.ResourceContexts)
            {
                results.Add(resourceContext.Name, resourceContext.Context.Set<Template>());
            }
            return results;
        }
        public IQueryable<Template> All(string resource)
        {
            var results = this.GetAndValidateResource(resource);
            return results.Context.Set<Template>();
        }
        public ResultSet<Template> GetWithResourceContext(string resource, int id)
        {
            var results = GetAndValidateResource(resource);

            var template = results
                .Context.Set<Template>()
                .FirstOrDefault(o => o.Id == id);
            if (template == null)
            {
                throw new TemplateDoesNotExistException("Content with ID " + id + " does not exist.");
            }
            var result = new ResultSet<Template>("Templates");
            result.Add(resource, template);
            return result;
        }

        public Template Create(string resource, Template entity)
        {
            var results = GetAndValidateResource(resource);

            results.Context.Set<Template>().Add(entity);
            this.SetTimeStamps(ref entity);
            results.Context.SaveChanges();

            return entity;
        }

        public void Delete(string resource, int id)
        {
            var results = GetAndValidateResource(resource);

            var dbEntry = this.Get(resource, id);

            results.Context.Set<Template>().Remove(dbEntry);
            results.Context.SaveChanges();
        }

        public Template Get(string resource, int id)
        {
            var results = GetAndValidateResource(resource);
            var template = results
                .Context.Set<Template>()
                .FirstOrDefault(o => o.Id == id);
            if (template == null)
            {
                throw new TemplateDoesNotExistException("Template with ID " + id + " does not exist");
            }

            return template;
        }

        public Template PartialUpdate(string resource, int id, Delta<Template> entity)
        {
            var results = GetAndValidateResource(resource);
            var dbEntry = this.Get(resource, id);
            entity.Patch(dbEntry);
            results.Context.Entry(dbEntry).Property("CreatedAt").IsModified = false;

            results.Context.SaveChanges();

            return dbEntry;
        }

        public Template Update(string resource, Template entity)
        {
            var results = GetAndValidateResource(resource);

            var dbEntry = this.Get(resource, entity.Id);

            results.Context.Entry(dbEntry).CurrentValues.SetValues(entity);
            results.Context.Entry(dbEntry).Property("CreatedAt").IsModified = false;

            results.Context.SaveChanges();
            return dbEntry;
        }
        public Template AddContent(string resource, int templateId, Content content)
        {
            var results = GetAndValidateResource(resource);
            var template = Get(resource, templateId);
            int i = 1;
            foreach (var cont in template.Contents)
            {
                i++;
            }
            content.Order = i;
            template.Contents.Add(content);
            results.Context.SaveChanges();
            return template;
        }
        public Template SetContent(string resource, Template template, ICollection<Content> contents)
        {
            var results = GetAndValidateResource(resource);
            var dbEntry = this.Get(resource, template.Id);
            dbEntry.Contents.Clear();
            int i = 1;
            foreach (var content in contents)
            {
                content.Order = i;
                dbEntry.Contents.Add(content);
                i++;
            }
            results.Context.SaveChanges();
            return dbEntry;
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
        private void SetTimeStamps(ref Template entity)
        {
            if (entity.CreatedAt == new DateTime())
            {
                entity.CreatedAt = DateTime.Now;
            }
            entity.UpdatedAt = DateTime.Now;
        }
    }
}