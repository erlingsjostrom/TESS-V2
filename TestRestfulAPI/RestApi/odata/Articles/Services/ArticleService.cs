using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.OData;
using TestRestfulAPI.Entities.TESS;
using TestRestfulAPI.Infrastructure.Database;
using TestRestfulAPI.Infrastructure.Services;
using TestRestfulAPI.RestApi.odata.Articles.Repositories;
using TestRestfulAPI.RestApi.odata.Users.Services;
using ResourceContext = TestRestfulAPI.Infrastructure.Database.ResourceContext;

namespace TestRestfulAPI.RestApi.odata.Articles.Services
{
    public class ArticleService : IService<Article, int, string>
    {
        private readonly UserService _userService;

        public ArticleService(UserService userService)
        {
            this._userService = userService;
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

        public Article PartialUpdate(string resource, int id, Delta<Article> article)
        {
            var articleRepository = this.GetArticleRepository();
            return articleRepository.PartialUpdate(resource, id, article);
        }

        public void Delete(string resource, int id)
        {
            var articleRepository = this.GetArticleRepository();
            articleRepository.Delete(resource, id);
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