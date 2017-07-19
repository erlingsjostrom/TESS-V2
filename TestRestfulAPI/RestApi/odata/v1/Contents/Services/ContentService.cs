﻿using System.Linq;
using System.Web;
using System.Web.OData;
using TestRestfulAPI.Infrastructure.Contexts;
using TestRestfulAPI.Infrastructure.Database;
using TestRestfulAPI.Infrastructure.Services;
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
        private OfferRepository _offerRepository;
        private TemplateRepository _templateRepository;

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

        public Content AddToTemplate(string resource, int contentId, int templateId)
        {
            this.InitRepository();
            var template = this._templateRepository.Get(resource, templateId);
            return _contentRepository.AddToTemplate(resource, contentId, template);
        }

        public Content AddToOffer(string resource, int contentId, int offerId)
        {
            this.InitRepository();
            var offer = this._offerRepository.Get(resource, offerId);
            return _contentRepository.AddToOffer(resource, contentId, offer);
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
            this._offerRepository = new OfferRepository(resourceContexts);
            this._templateRepository = new TemplateRepository(resourceContexts);
        }
    }
}