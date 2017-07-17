using System.Linq;
using System.Web;
using System.Web.OData;
using TestRestfulAPI.Infrastructure.Contexts;
using TestRestfulAPI.Infrastructure.Database;
using TestRestfulAPI.Infrastructure.Services;
using TestRestfulAPI.RestApi.odata.v1.Contents.Entities;
using TestRestfulAPI.RestApi.odata.v1.Contents.Repositories;
using TestRestfulAPI.RestApi.odata.v1.Users.Services;
using ResourceContext = TestRestfulAPI.Infrastructure.Database.ResourceContext;

namespace TestRestfulAPI.RestApi.odata.v1.Contents.Services
{
    public class TextItemService : IService<TextItem, int, string>
    {
        private readonly UserService _userService;
        private TextItemRepository _textitemRepository;
        private ContentRepository _contentRepository;

        public TextItemService(UserService userService)
        {
            this._userService = userService;
        }
        public IQueryable<TextItem> All(string resource)
        {
            this.InitRepository();
            return _textitemRepository.All(resource);
        }

        public TextItem Get(string resource, int id)
        {
            this.InitRepository();
            return _textitemRepository.Get(resource, id);
        }

        public TextItem Create(string resource, TextItem textitem)
        {
            this.InitRepository();
            return _textitemRepository.Create(resource, textitem);
        }

        public TextItem Update(string resource, TextItem textitem)
        {
            this.InitRepository();
            return _textitemRepository.Update(resource, textitem);
        }

        public TextItem PartialUpdate(string resource, int id, Delta<TextItem> textitem)
        {
            this.InitRepository();
            return _textitemRepository.PartialUpdate(resource, id, textitem);
        }

        public void Delete(string resource, int id)
        {
            this.InitRepository();
            _textitemRepository.Delete(resource, id);
        }
        public TextItem CreateContentTextItem(string resource, TextItem textitem, int contentId)
        {
            this.InitRepository();
            var content = this._contentRepository.Get(resource, contentId);
            return _textitemRepository.CreateContentTextItem(resource, textitem, content);
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

            this._textitemRepository = new TextItemRepository(resourceContexts);
            this._contentRepository = new ContentRepository(resourceContexts);
        }
    }
}