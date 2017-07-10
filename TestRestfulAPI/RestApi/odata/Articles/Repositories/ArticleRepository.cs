using System.Collections.Generic;
using System.Linq;
using System.Web.OData;
using TestRestfulAPI.Entities.TESS;
using TestRestfulAPI.Infrastructure.Exceptions;
using TestRestfulAPI.Infrastructure.Repositories;
using TestRestfulAPI.RestApi.odata.Articles.Exceptions;
using ResourceContext = TestRestfulAPI.Infrastructure.Database.ResourceContext;

namespace TestRestfulAPI.RestApi.odata.Articles.Repositories
{
    public class ArticleRepository : BaseRepository<Article>, IRepository<Article, int, string>
    {
        public ArticleRepository(IEnumerable<ResourceContext> resourceContexts) : base(resourceContexts)
        {
        }

        public IEnumerable<IQueryable<Article>> All()
        {
            var results = new List<IQueryable<Article>>();
            foreach (var resourceContext in this.ResourceContexts)
            {
                results.Add(resourceContext.Context.Set<Article>());
            }
            return results; 
        }

        public ResultSet<IQueryable<Article>> AllWithResourceContext()
        {
            var results = new ResultSet<IQueryable<Article>>("Articles");
            foreach (var resourceContext in this.ResourceContexts)
            {
                results.Add(resourceContext.Name, resourceContext.Context.Set<Article>());
            }
            return results;
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
                throw new ArticleDoesNotExistException("Article with ID " + id + " does not exist");
            }

            return article;
        }

        public ResultSet<Article> GetWithResourceContext(string resource, int id)
        {
            var results = GetAndValidateResource(resource);

            var article = results
                .Context.Set<Article>()
                .FirstOrDefault(a => a.Id == id);
            if (article == null)
            {
                throw new ArticleDoesNotExistException("Article with ID " + id + " does not exist");
            }
            var result = new ResultSet<Article>("Articles");
            result.Add(resource, article);
            return result;
        }

        public Article Create(string resource, Article entity)
        {
            var results = GetAndValidateResource(resource);

            results.Context.Set<Article>().Add(entity);
            results.Context.SaveChanges();

            return entity;
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