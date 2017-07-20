using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.OData;
using TestRestfulAPI.Infrastructure.Exceptions;
using TestRestfulAPI.Infrastructure.Repositories;
using TestRestfulAPI.RestApi.odata.v1.Articles.Entities;
using TestRestfulAPI.RestApi.odata.v1.Contents.Entities;
using TestRestfulAPI.RestApi.odata.v1.Contents.Exceptions;
using TestRestfulAPI.RestApi.odata.v1.Offers.Entities;
using ResourceContext = TestRestfulAPI.Infrastructure.Database.ResourceContext;

namespace TestRestfulAPI.RestApi.odata.v1.Contents.Repositories
{
    public class ContentRepository : BaseRepository<Content>, IRepository<Content, int, string>
    {
        public ContentRepository(IEnumerable<ResourceContext> resourceContexts) : base(resourceContexts)
        {
        }

        public IEnumerable<IQueryable<Content>> All()
        {
            var results = new List<IQueryable<Content>>();
            foreach (var resourceContext in this.ResourceContexts)
            {
                results.Add(resourceContext.Context.Set<Content>());
            }
            return results;
        }
        public ResultSet<IQueryable<Content>> AllWithResourceContext()
        {
            var results = new ResultSet<IQueryable<Content>>("Contents");
            foreach (var resourceContext in this.ResourceContexts)
            {
                results.Add(resourceContext.Name, resourceContext.Context.Set<Content>());
            }
            return results;
        }
        public IQueryable<Content> All(string resource)
        {
            var results = this.GetAndValidateResource(resource);
            return results.Context.Set<Content>();
        }
        public ResultSet<Content> GetWithResourceContext(string resource, int id)
        {
            var results = GetAndValidateResource(resource);

            var content = results
                .Context.Set<Content>()
                .FirstOrDefault(o => o.Id == id);
            if (content == null)
            {
                throw new ContentDoesNotExistException("Content with ID " + id + " does not exist.");
            }
            var result = new ResultSet<Content>("Contents");
            result.Add(resource, content);
            return result;
        }

        public Content Create(string resource, Content entity)
        {
            var results = GetAndValidateResource(resource);

            results.Context.Set<Content>().Add(entity);
            this.SetTimeStamps(ref entity);
            results.Context.SaveChanges();

            return entity;
        }

        public void Delete(string resource, int id)
        {
            var results = GetAndValidateResource(resource);

            var dbEntry = this.Get(resource, id);

            results.Context.Set<Content>().Remove(dbEntry);
            results.Context.SaveChanges();
        }

        public Content Get(string resource, int id)
        {
            var results = GetAndValidateResource(resource);
            var content = results
                .Context.Set<Content>()
                .FirstOrDefault(o => o.Id == id);
            if (content == null)
            {
                throw new ContentDoesNotExistException("Content with ID " + id + " does not exist");
            }

            return content;
        }

        public Content PartialUpdate(string resource, int id, Delta<Content> entity)
        {
            var results = GetAndValidateResource(resource);
            var dbEntry = this.Get(resource, id);
            entity.Patch(dbEntry);
            results.Context.Entry(dbEntry).Property("CreatedAt").IsModified = false;

            results.Context.SaveChanges();

            return dbEntry;
        }

        public Content Update(string resource, Content entity)
        {
            var results = GetAndValidateResource(resource);

            var dbEntry = this.Get(resource, entity.Id);

            results.Context.Entry(dbEntry).CurrentValues.SetValues(entity);
            results.Context.Entry(dbEntry).Property("CreatedAt").IsModified = false;

            dbEntry.TextItems.Clear();
            foreach (var textitem in entity.TextItems)
            {
                dbEntry.TextItems.Add(results.Context.Set<TextItem>().ToList().FirstOrDefault(r => r.Id == textitem.Id));
            }
            dbEntry.Articles.Clear();
            foreach (var article in entity.Articles)
            {
                dbEntry.Articles.Add(results.Context.Set<Article>().ToList().FirstOrDefault(r => r.Id == article.Id));
            }


            results.Context.SaveChanges();
            return dbEntry;
        }
        public Content AddTextItem(string resource, int contentId, TextItem textitem)
        {
            var results = GetAndValidateResource(resource);
            var content = Get(resource, contentId);
            content.TextItems.Add(textitem);
            results.Context.SaveChanges();
            return content;
        }
        public Content AddArticle(string resource, int contentId, Article article)
        {
            var results = GetAndValidateResource(resource);
            var content = Get(resource, contentId);
            content.Articles.Add(article);
            results.Context.SaveChanges();
            return content;
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
        private void SetTimeStamps(ref Content entity)
        {
            if (entity.CreatedAt == new DateTime())
            {
                entity.CreatedAt = DateTime.Now;
            }
            entity.UpdatedAt = DateTime.Now;
        }
    }
}