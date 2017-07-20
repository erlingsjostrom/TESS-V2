using System.Linq;
using System.Web;
using System.Web.OData;
using TestRestfulAPI.Infrastructure.Contexts;
using TestRestfulAPI.Infrastructure.Database;
using TestRestfulAPI.Infrastructure.Services;
using TestRestfulAPI.RestApi.odata.v1.Articles.Repositories;
using TestRestfulAPI.RestApi.odata.v1.Contents.Entities;
using TestRestfulAPI.RestApi.odata.v1.Contents.Repositories;
using TestRestfulAPI.RestApi.odata.v1.Offers.Repositories;
using TestRestfulAPI.RestApi.odata.v1.Users.Services;
using ResourceContext = TestRestfulAPI.Infrastructure.Database.ResourceContext;

namespace TestRestfulAPI.RestApi.odata.v1.Contents.Services
{
    public class ContentService : IService<Content, int, string>
    {
        private readonly UserService _userService;
        private ContentRepository _contentRepository;
        private TextItemRepository _textitemRepository;
        private ArticleRepository _articleRepository;

        public ContentService(UserService userService)
        {
            this._userService = userService;
        }
        public IQueryable<Content> All(string resource)
        {
            this.InitRepository();
            return _contentRepository.All(resource);
        }

        public Content Create(string resource, Content content)
        {
            this.InitRepository();
            return _contentRepository.Create(resource, content);
        }

        public void Delete(string resource, int id)
        {
            this.InitRepository();
            _contentRepository.Delete(resource, id);
        }

        public Content Get(string resource, int id)
        {
            this.InitRepository();
            return _contentRepository.Get(resource, id);
        }

        public Content PartialUpdate(string resource, int id, Delta<Content> content)
        {
            this.InitRepository();
            return _contentRepository.PartialUpdate(resource, id, content);
        }

        public Content Update(string resource, Content content)
        {
            this.InitRepository();
            return _contentRepository.Update(resource, content);
        }
        public Content AddTextItem(string resource, int contentId, int textitemId)
        {
            this.InitRepository();
            var textitem = this._textitemRepository.Get(resource, textitemId);
            return _contentRepository.AddTextItem(resource, contentId, textitem);
        }
        public Content AddArticle(string resource, int contentId, int articleId)
        {
            this.InitRepository();
            var article = this._articleRepository.Get(resource, articleId);
            return _contentRepository.AddArticle(resource, contentId, article);
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

            this._contentRepository = new ContentRepository(resourceContexts);
            this._textitemRepository = new TextItemRepository(resourceContexts);
            this._articleRepository = new ArticleRepository(resourceContexts);
        }
    }
}