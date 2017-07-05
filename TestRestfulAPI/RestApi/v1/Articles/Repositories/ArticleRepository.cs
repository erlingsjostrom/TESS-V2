using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestRestfulAPI.Entities.TESS;
using TestRestfulAPI.Infrastructure.Database;
using TestRestfulAPI.Infrastructure.Repositories;
using TestRestfulAPI.RestApi.v1.Articles.Exceptions;

namespace TestRestfulAPI.RestApi.v1.Articles.Repositories
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

        public IQueryable<Article> All(string resource)
        {
            var results = this.ResourceContexts.FirstOrDefault(c => c.Name == resource);
            if (results == null)
            {
                throw new InvalidDbContextTypeException("Resource not found");
            }
            return results.Context.Set<Article>();
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

        public IEnumerable<IQueryable<Article>> Get(int id, string resource)
        {
            throw new NotImplementedException();
        }

        public ResultSet<Article> GetWithResourceContext(int id, string resource)
        {
            var results = this.ResourceContexts.FirstOrDefault(c => c.Name == resource);
            if (results == null)
            {
                throw new InvalidDbContextTypeException("Resource not found");
            }
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

        public Article Create(Article entity, string resource)
        {
            var results = this.ResourceContexts.FirstOrDefault(c => c.Name == resource);
            if (results == null)
            {
                throw new InvalidDbContextTypeException("Resource not found");
            }
            results.Context.Set<Article>().Add(entity);
            results.Context.SaveChanges();

            return entity;
        }
    }
}