using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.OData;
using TestRestfulAPI.Infrastructure.Contexts;
using TestRestfulAPI.Infrastructure.Database;
using TestRestfulAPI.Infrastructure.Services;
using TestRestfulAPI.RestApi.odata.v1.Articles.Repositories;
using TestRestfulAPI.RestApi.odata.v1.Contents.Entities;
using TestRestfulAPI.RestApi.odata.v1.Contents.Repositories;
using TestRestfulAPI.RestApi.odata.v1.Users.Services;
using ResourceContext = TestRestfulAPI.Infrastructure.Database.ResourceContext;

namespace TestRestfulAPI.RestApi.odata.v1.Contents.Services
{
    public class TemplateService : IService<Template, int, string>
    {
        private readonly UserService _userService;
        private TemplateRepository _templateRepository;
        private ContentRepository _contentRepository;
        private ArticleRepository _articleRepository;
        private TextItemRepository _textitemRepository;

        public TemplateService(UserService userService)
        {
            this._userService = userService;
        }
        public IQueryable<Template> All(string resource)
        {
            this.InitRepository();
            return _templateRepository.All(resource);
        }

        public Template Get(string resource, int id)
        {
            this.InitRepository();
            return _templateRepository.Get(resource, id);
        }

        public Template Create(string resource, Template template)
        {
            this.InitRepository();
            return _templateRepository.Create(resource, template);
        }

        public Template Update(string resource, Template template)
        {
            this.InitRepository();
            return _templateRepository.Update(resource, template);
        }

        public Template PartialUpdate(string resource, int id, Delta<Template> template)
        {
            this.InitRepository();
            return _templateRepository.PartialUpdate(resource, id, template);
        }

        public void Delete(string resource, int id)
        {
            this.InitRepository();
            _templateRepository.Delete(resource, id);
        }

        public Template AddContent(string resource, int templateId, int contentId)
        {
            this.InitRepository();
            var content = this._contentRepository.Get(resource, contentId);
            var newContent = this._contentRepository.CreateCopy(resource, content);
            return _templateRepository.AddContent(resource, templateId, newContent);
        }

        public Template SetContent(string resource, Template template)
        {
            this.InitRepository();
            List<Content> contents = new List<Content>();

            foreach (var content in template.Contents)
            {
                var dbContent = _contentRepository.Get(resource, content.Id);
                var newContent = _contentRepository.CreateCopy(resource, dbContent);
                newContent.EntityType = "Template";
                foreach (var article in newContent.Articles)
                {
                    article.EntityType = "Template";
                    
                }
                contents.Add(newContent);
            }
            return _templateRepository.SetContent(resource, template, contents);
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

            this._templateRepository = new TemplateRepository(resourceContexts);
            this._contentRepository = new ContentRepository(resourceContexts);
            this._articleRepository = new ArticleRepository(resourceContexts);
            this._textitemRepository = new TextItemRepository(resourceContexts);
        }
    }
}