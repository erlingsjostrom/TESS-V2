using System;
using System.Linq;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Routing;
using Microsoft.Web.Http;
using TestRestfulAPI.Infrastructure.Authorization.Attributes;
using TestRestfulAPI.Infrastructure.Controllers;
using TestRestfulAPI.RestApi.odata.v1.Offers.Entities;
using TestRestfulAPI.RestApi.odata.v1.Offers.Services;

namespace TestRestfulAPI.RestApi.odata.v1.Offers.Controllers
{
    [ApiVersion("1.0")]
    [ODataRoutePrefix("Contents")]
    [UserHasRole("UserControl")]
    public class ContentController : ResourceODataController, ICrudController<Content>
    {
        private readonly ContentService _contentService = GlobalServices.ContentService;

        // POST: {resource}/Contents()
        [UserHasResourceAccess, UserHasPermission("Write")]
        [EnableQuery, HttpPost, ODataRoute("()")]
        public IHttpActionResult Create([FromBody] Content content)
        {
            this.ParseResource();
            return ODataCreated(this._contentService.Create(this.Resource, content), content.Id);
        }

        // DELETE: {resource}/Contents({id})
        [UserHasResourceAccess, UserHasPermission("Remove")]
        [EnableQuery, HttpDelete, ODataRoute("({id})")]
        public void Delete(int id)
        {
            this.ParseResource();
            this._contentService.Delete(this.Resource, id);
            this.ODataDeleted(); // Set response headers
        }

        // GET: {resource}/Contents()
        [EnableQuery, HttpGet, ODataRoute()]
        public IQueryable<Content> Get()
        {
            this.ParseResource();
            return this._contentService.All(this.Resource);
        }

        // GET: {resource}/Contents({id})
        [UserHasResourceAccess, UserHasPermission("Read")]
        [EnableQuery, HttpGet, ODataRoute("({id})")]
        public Content Get(int id)
        {
            this.ParseResource();
            return this._contentService.Get(this.Resource, id);
        }

        // PATCH: {resource}/Contents({id})
        [UserHasResourceAccess, UserHasPermission("Modify")]
        [EnableQuery, HttpPatch, ODataRoute("({id})")]
        public Content PartialUpdate(int id, [FromBody] Delta<Content> content)
        {
            this.ParseResource();
            return this._contentService.PartialUpdate(this.Resource, id, content);
        }

        // PUT: {resource}/Contents({id})
        [UserHasResourceAccess, UserHasPermission("Modify")]
        [EnableQuery, HttpPut, ODataRoute("({id})")]
        public Content Update(int id, [FromBody] Content content)
        {
            this.ParseResource();
            return this._contentService.Update(this.Resource, content);
        }
    }
}