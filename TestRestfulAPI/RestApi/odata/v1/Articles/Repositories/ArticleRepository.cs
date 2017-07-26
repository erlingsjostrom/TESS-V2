using System.Collections.Generic;
using System.Linq;
using System.Web.OData;
using TestRestfulAPI.Infrastructure.Exceptions;
using TestRestfulAPI.Infrastructure.Repositories;
using TestRestfulAPI.RestApi.odata.v1.Articles.Entities;
using TestRestfulAPI.RestApi.odata.v1.Articles.Exceptions;
using ResourceContext = TestRestfulAPI.Infrastructure.Database.ResourceContext;

namespace TestRestfulAPI.RestApi.odata.v1.Articles.Repositories
{
    public class ArticleRepository : BaseRepository<Article>, IRepository<Article, int, string>
    {
        public ArticleRepository(IEnumerable<ResourceContext> resourceContexts) : base(resourceContexts)
        {
        }

        public IQueryable<Article> All(string resource)
        {
            var results = this.GetAndValidateResource(resource);
            return results.Context.Set<Article>();
        }

        public Article Get(string resource, int id)
        {
            var results = GetAndValidateResource(resource);
            var article = results
                .Context.Set<Article>()
                .FirstOrDefault(a => a.Id == id);
            if (article == null)
            {
                throw new ArticleDoesNotExistException("Article with ID " + id + " does not exist.");
            }

            return article;
        }

        public Article Create(string resource, Article entity)
        {
            var results = GetAndValidateResource(resource);
            var article = this.All(resource).FirstOrDefault(a => a.ArticleNumber == entity.ArticleNumber);
            if (article != null)
            {
                throw new ArticleAlreadyExistException("Article with Article number " + entity.ArticleNumber +
                                                       " does already exists.");
            }
            entity.EntityType = "Article";
            results.Context.Set<Article>().Add(entity);
            results.Context.SaveChanges();

            return entity;
        }

        public Article CreateCopy(string resource, Article entity)
        {
            var results = GetAndValidateResource(resource);
            var newEntity = results.Context.Set<Article>()
                .AsNoTracking()
                //.Include(x => x.)
                .FirstOrDefault(x => x.Id == entity.Id);

            results.Context.Set<Article>().Add(newEntity);
            results.Context.SaveChanges();

            return newEntity;
        }

        public Article Update(string resource, Article entity)
        {
            var results = GetAndValidateResource(resource);

            var dbEntry = this.Get(resource, entity.Id);
            
            results.Context.Entry(dbEntry).CurrentValues.SetValues(entity);
            results.Context.Entry(dbEntry).Property("CreatedAt").IsModified = false;
            
            results.Context.SaveChanges();

            return dbEntry;
        }

        public Article PartialUpdate(string resource, int id, Delta<Article> entity)
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

            results.Context.Set<Article>().Remove(dbEntry);
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
    }
}