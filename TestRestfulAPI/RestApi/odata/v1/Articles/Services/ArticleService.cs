using System.Linq;
using System.Web;
using System.Web.OData;
using TestRestfulAPI.Infrastructure.Contexts;
using TestRestfulAPI.Infrastructure.Database;
using TestRestfulAPI.Infrastructure.Services;
using TestRestfulAPI.RestApi.odata.v1.Articles.Entities;
using TestRestfulAPI.RestApi.odata.v1.Articles.Repositories;
using TestRestfulAPI.RestApi.odata.v1.Users.Services;
using ResourceContext = TestRestfulAPI.Infrastructure.Database.ResourceContext;

namespace TestRestfulAPI.RestApi.odata.v1.Articles.Services
{
    public class ArticleService : IService<Article, int, string>
    {
        private readonly UserService _userService;
        private ArticleRepository _articleRepository;

        public ArticleService(UserService userService)
        {
            this._userService = userService;
        }

        public IQueryable<Article> All(string resource)
        {
            this.InitRepository();
            return _articleRepository.All(resource);
        }

        public Article Get(string resource, int id)
        {
            this.InitRepository();
            return _articleRepository.Get(resource, id);
        }

        public Article Create(string resource, Article article)
        {
            this.InitRepository();
            return _articleRepository.Create(resource, article);
        }

        public Article Update(string resource, Article article)
        {
            this.InitRepository();
            return _articleRepository.Update(resource, article);
        }

        public Article PartialUpdate(string resource, int id, Delta<Article> article)
        {
            this.InitRepository();
            return _articleRepository.PartialUpdate(resource, id, article);
        }

        public void Delete(string resource, int id)
        {
            this.InitRepository();
            _articleRepository.Delete(resource, id);
        }

        private void InitRepository()
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

            this._articleRepository = new ArticleRepository(resourceContexts);
        }

        
    }
}