using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TestRestfulAPI.Entities.TESS;
using TestRestfulAPI.Infrastructure.Database;
using TestRestfulAPI.Infrastructure.Exceptions;
using TestRestfulAPI.Infrastructure.Repositories;
using TestRestfulAPI.Infrastructure.Exceptions;
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

        // TODO: FIX CREATED_AT CONSTRAINT
        public Article Update(string resource, Article entity)
        {
            var results = GetAndValidateResource(resource);

            var dbEntry = this.Get(resource, entity.Id);
            
            results.Context.Entry(dbEntry).CurrentValues.SetValues(entity);
            
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
    }
}