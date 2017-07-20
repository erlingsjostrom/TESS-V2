using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Routing;
using Microsoft.Web.Http;
using TestRestfulAPI.Infrastructure.Authorization.Attributes;
using TestRestfulAPI.Infrastructure.Controllers;
using TestRestfulAPI.RestApi.odata.v1.Contents.Entities;
using TestRestfulAPI.RestApi.odata.v1.Contents.Services;

namespace TestRestfulAPI.RestApi.odata.v1.Contents.Controllers
{
    [ApiVersion("1.0")]
    [ODataRoutePrefix("Templates")]
    public class TemplateController : ResourceODataController, ICrudController<Template>
    {
        private readonly TemplateService _templateService = GlobalServices.TemplateService;

        // GET: {resource}/Templates()
        [EnableQuery, HttpGet, ODataRoute()]
        public IQueryable<Template> Get()
        {
            this.ParseResource();
            return this._templateService.All(this.Resource);
        }

        // GET: {resource}/Templates({id})
        [UserHasResourceAccess, UserHasPermission("Read")]
        [EnableQuery, HttpGet, ODataRoute("({id})")]
        public Template Get(int id)
        {
            this.ParseResource();
            return this._templateService.Get(this.Resource, id);
        }

        // POST: {resource}/Templates()
        [UserHasResourceAccess, UserHasPermission("Write")]
        [EnableQuery, HttpPost, ODataRoute("()")]
        public IHttpActionResult Create(Template template)
        {
            this.ParseResource();
            return ODataCreated(this._templateService.Create(this.Resource, template), template.Id);
        }

        // PUT: {resource}/Templates({id})
        [UserHasResourceAccess, UserHasPermission("Modify")]
        [EnableQuery, HttpPut, ODataRoute("({id})")]
        public Template Update(int id, [FromBody] Template template)
        {
            this.ParseResource();
            return this._templateService.Update(this.Resource, template);
        }

        // PATCH: {resource}/Templates({id})
        [UserHasResourceAccess, UserHasPermission("Modify")]
        [EnableQuery, HttpPatch, ODataRoute("({id})")]
        public Template PartialUpdate(int id, Delta<Template> template)
        {
            this.ParseResource();
            return this._templateService.PartialUpdate(this.Resource, id, template);
        }

        // DELETE: {resource}/Templates({id})
        [UserHasResourceAccess, UserHasPermission("Remove")]
        [EnableQuery, HttpDelete, ODataRoute("({id})")]
        public void Delete(int id)
        {
            this.ParseResource();
            this._templateService.Delete(this.Resource, id);
            this.ODataDeleted();
        }

        // PUT: {resource}/Contents({id})
        [UserHasResourceAccess, UserHasPermission("Modify")]
        [EnableQuery, HttpPut, ODataRoute("({templateId})/Contents({contentId})")]
        public Template AddContent(int templateId, int contentId) 
        {
            this.ParseResource();
            return this._templateService.AddContent(this.Resource, templateId, contentId);
        }
    }
}