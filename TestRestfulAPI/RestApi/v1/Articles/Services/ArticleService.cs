using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;
using TestRestfulAPI.Entities.TESS;
using TestRestfulAPI.Entities.User;
using TestRestfulAPI.Infrastructure.Database;
using TestRestfulAPI.Infrastructure.Repositories;
using TestRestfulAPI.RestApi.v1.Articles.Repositories;
using TestRestfulAPI.RestApi.v1.Users.Repositories;
using TestRestfulAPI.RestApi.v1.Users.Services;

namespace TestRestfulAPI.RestApi.v1.Articles.Services
{
    public class ArticleService
    {
        private readonly UserService _userService;

        public ArticleService(UserService userService)
        {
            this._userService = userService;
        }

        public IEnumerable<IQueryable<Article>> All()
        {
            var articleRepository = this.GetArticleRepository();
            return articleRepository.All();
        }

        public ResultSet<IQueryable<Article>> AllWithResourceContext()
        {
            var articleRepository = this.GetArticleRepository();
            return articleRepository.AllWithResourceContext();
        }

        public IQueryable<Article> All(string resource)
        {
            var articleRepository = this.GetArticleRepository();
            return articleRepository.All(resource);
        }

        public Article Get(string resource, int id)
        {
            var articleRepository = this.GetArticleRepository();
            return articleRepository.Get(resource, id);
        }

        public Article Create(string resource, Article article)
        {
            var articleRepository = this.GetArticleRepository();
            return articleRepository.Create(resource, article);
        }

        public Article Update(string resource, Article article)
        {
            var articleRepository = this.GetArticleRepository();
            return articleRepository.Update(resource, article);
        }

        private ArticleRepository GetArticleRepository()
        {
            var userName = HttpContext.Current.User.Identity.Name;
            var user = this._userService.GetByWindowsIdentityName(userName);

            var resourceContexts = user.Resources
                                       .Select(resource => 
                                            new ResourceContext(
                                                resource.Name, 
                                                DbContextFactory.Get<TESSEntities>(resource.Location), 
                                                typeof(TESSEntities)
                                            )
                                        ).ToList();

            return new ArticleRepository(resourceContexts);
        }

        
    }
}