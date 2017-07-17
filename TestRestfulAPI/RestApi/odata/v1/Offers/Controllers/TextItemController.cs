using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Routing;
using Microsoft.Web.Http;
using TestRestfulAPI.Infrastructure.Controllers;
using TestRestfulAPI.RestApi.odata.v1.Offers.Entities;
using TestRestfulAPI.RestApi.odata.v1.Offers.Services;
using TestRestfulAPI.Infrastructure.Authorization.Attributes;

namespace TestRestfulAPI.RestApi.odata.v1.Offers.Controllers
{
    [ApiVersion("1.0")]
    [ODataRoutePrefix("Contents")]
    public class TextItemController : ResourceODataController, ICrudController<TextItem>
    {
        private readonly TextItemService _textitemService = GlobalServices.TextItemService;
        
        // GET: {resource}/TextItems()
        [EnableQuery, HttpGet, ODataRoute()]
        public IQueryable<TextItem> Get()
        {
            this.ParseResource();
            return this._textitemService.All(this.Resource);
        }

        // GET: {resource}/TextItems({id})
        [UserHasResourceAccess, UserHasPermission("Read")]
        [EnableQuery, HttpGet, ODataRoute("({id})")]
        public TextItem Get(int id)
        {
            this.ParseResource();
            return this._textitemService.Get(this.Resource, id);
        }

        // POST: {resource}/TextItems()
        [UserHasResourceAccess, UserHasPermission("Write")]
        [EnableQuery, HttpPost, ODataRoute("()")]
        public IHttpActionResult Create([FromBody] TextItem textitem)
        {
            this.ParseResource();
            return ODataCreated(this._textitemService.Create(this.Resource, textitem), textitem.Id);
        }

        // PUT: {resource}/TextItems({id})
        [UserHasResourceAccess, UserHasPermission("Modify")]
        [EnableQuery, HttpPut, ODataRoute("({id})")]
        public TextItem Update(int id, [FromBody] TextItem textitem)
        {
            this.ParseResource();
            return this._textitemService.Update(this.Resource, textitem);
        }

        // PATCH: {resource}/TextItems({id})
        [UserHasResourceAccess, UserHasPermission("Modify")]
        [EnableQuery, HttpPatch, ODataRoute("({id})")]
        public TextItem PartialUpdate(int id, [FromBody] Delta<TextItem> textitem)
        {
            this.ParseResource();
            return this._textitemService.PartialUpdate(this.Resource, id, textitem);
        }

        // DELETE: {resource}/TextItems({id})
        [UserHasResourceAccess, UserHasPermission("Remove")]
        [EnableQuery, HttpDelete, ODataRoute("({id})")]
        public void Delete(int id)
        {
            this.ParseResource();
            this._textitemService.Delete(this.Resource, id);
            this.ODataDeleted(); // Set response headers
        }
    }
}