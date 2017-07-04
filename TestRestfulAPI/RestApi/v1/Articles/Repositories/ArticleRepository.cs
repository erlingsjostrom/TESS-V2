﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestRestfulAPI.Entities.TESS;
using TestRestfulAPI.Infrastructure.Repositories;

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
            var results = new ResultSet<IQueryable<Article>>();
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
                throw new Exception("Resource not found");
            }
            var article = results
                .Context.Set<Article>()
                .FirstOrDefault(a => a.Id == id);
            if (article == null)
            {
                throw new Exception("Resource not found");
            }
            var result = new ResultSet<Article>();
            result.Add(resource, article);
            return result;
        }

        public Article Create(Article entity, string resource)
        {
            var results = this.ResourceContexts.FirstOrDefault(c => c.Name == resource);
            if (results == null)
            {
                throw new Exception("Resource not found");
            }
            results.Context.Set<Article>().Add(entity);
            results.Context.SaveChanges();

            return entity;
        }
    }
}