using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Routing;
using Microsoft.Web.Http;
using TestRestfulAPI.Infrastructure.Authorization.Attributes;
using TestRestfulAPI.Infrastructure.Controllers;
using TestRestfulAPI.RestApi.odata.v1.Offers.Entities;
using TestRestfulAPI.RestApi.odata.v1.Offers.Services;
using TestRestfulAPI.RestApi.odata.v1.Contents.Entities;

namespace TestRestfulAPI.RestApi.odata.v1.Offers.Controllers
{
    [ApiVersion("1.0")]
    [ODataRoutePrefix("Offers")]
    [UserHasPermission("OfferAccess")]
    public class OfferController : ResourceODataController, ICrudController<Offer>
    {
        private readonly OfferService _offerService = GlobalServices.OfferService;

        // GET: {resource}/Offers()
        [UserHasResourceAccess, UserHasPermission("Read")]
        [EnableQuery, HttpGet, ODataRoute()]
        public IQueryable<Offer> Get()
        {
            this.ParseResource();
            return this._offerService.All(this.Resource);
        }

        // GET: {resource}/Offers({id})
        [UserHasResourceAccess, UserHasPermission("Read")]
        [EnableQuery, HttpGet, ODataRoute("({id})")]
        public Offer Get(int id)
        {
            this.ParseResource();
            return this._offerService.Get(this.Resource, id);
        }

        // POST: {resource}/Offers({customerId})
        [UserHasResourceAccess, UserHasPermission("Write")]
        [EnableQuery, HttpPost, ODataRoute("()")]
        public IHttpActionResult Create([FromBody] Offer offer)
        {
            this.ParseResource();
            return ODataCreated(this._offerService.Create(this.Resource, offer), offer.Id);
        }
        // Put: {resource}/Offers({customerId})
        [UserHasResourceAccess, UserHasPermission("Write")]
        [EnableQuery, HttpPost, ODataRoute("({customerId})")]
        public IHttpActionResult CreateCustomerOffer(Offer offer, int customerId)
        {
            this.ParseResource();
            return ODataCreated(this._offerService.CreateCustomerOffer(this.Resource, offer, customerId), offer.Id);

        }

        // PUT: {resource}/Offers({id})
        [UserHasResourceAccess, UserHasPermission("Modify")]
        //[EnableQuery, HttpPut, ODataRoute("({id})")]
        [EnableQuery, HttpPut, ODataRoute("({id})")]
        public Offer Update(int id, [FromBody] Offer offer)
        {
            this.ParseResource();
            return this._offerService.Update(this.Resource, offer);
        }

        // PATCH: {resource}/Offers({id})
        [UserHasResourceAccess, UserHasPermission("Modify")]
        [EnableQuery, HttpPatch, ODataRoute("({id})")]
        public Offer PartialUpdate(int id, Delta<Offer> offer)
        {
            this.ParseResource();
            return this._offerService.PartialUpdate(this.Resource, id, offer);
        }

        // PUT: {resource}/Offers({id})
        [UserHasResourceAccess, UserHasPermission("Modify")]
        [EnableQuery, HttpPut, ODataRoute("({offerId})/Contents({contentId})")]
        public Offer AddContent(int offerId, int contentId)
        {
            this.ParseResource();
            return this._offerService.AddContent(this.Resource, offerId, contentId);
        }

        // DELETE: {resource}/Offers({id})
        [UserHasResourceAccess, UserHasPermission("Modify")]
        [EnableQuery, HttpDelete, ODataRoute("({offerId})/Contents({contentId})")]
        public Offer RemoveContent(int offerId, int contentId)
        {
            this.ParseResource();
            return this._offerService.RemoveContent(this.Resource, offerId, contentId);
        }

        [UserHasResourceAccess, UserHasPermission("Modify")]
        [EnableQuery, HttpPut, ODataRoute("()")]
        public Offer SetContent(Offer offer)
        {
            this.ParseResource();
            int id = 2;
            return this._offerService.SetContent(this.Resource, offer, id);
        }


        // DELETE: {resource}/Offers({id})
        [UserHasResourceAccess, UserHasPermission("Remove")]
        [EnableQuery, HttpDelete, ODataRoute("({id})")]
        public void Delete(int id)
        {
            this.ParseResource();
            this._offerService.Delete(this.Resource, id);
            this.ODataDeleted(); // Set response headers
        }
    }
}