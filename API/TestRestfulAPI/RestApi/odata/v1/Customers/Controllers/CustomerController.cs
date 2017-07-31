using System.Linq;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Routing;
using Microsoft.Web.Http;
using TestRestfulAPI.Infrastructure.Authorization.Attributes;
using TestRestfulAPI.Infrastructure.Controllers;
using TestRestfulAPI.RestApi.odata.v1.Customers.Entities;
using TestRestfulAPI.RestApi.odata.v1.Customers.Services;

namespace TestRestfulAPI.RestApi.odata.v1.Customers.Controllers
{
    [ApiVersion("1.0")]
    [ODataRoutePrefix("Customers")]
    [UserHasPermission("CustomerAccess")]
    public class CustomerController : ResourceODataController, ICrudController<Customer>
    {
        private readonly CustomerService _customerService = GlobalServices.CustomerService;

        // GET: {resource}/Customers()
        [UserHasResourceAccess, UserHasPermission("Read")]
        [EnableQuery, HttpGet, ODataRoute()]
        public IQueryable<Customer> Get()
        {
            this.ParseResource();
            return this._customerService.All(this.Resource);
        }

        // GET: {resource}/Customers({id})
        [UserHasResourceAccess, UserHasPermission("Read")]
        [EnableQuery, HttpGet, ODataRoute("({id})")]
        public Customer Get(int id)
        {
            this.ParseResource();
            return this._customerService.Get(this.Resource, id);
        }

        // POST: {resource}/Customers()
        [UserHasResourceAccess, UserHasPermission("Write")]
        [EnableQuery, HttpPost, ODataRoute("()")]
        public IHttpActionResult Create([FromBody] Customer customer)
        {
            this.ParseResource();
            return ODataCreated(this._customerService.Create(this.Resource, customer), customer.Id);
        }

        // PUT: {resource}/Customers({id})
        [UserHasResourceAccess, UserHasPermission("Modify")]
        [EnableQuery, HttpPut, ODataRoute("({id})")]
        public Customer Update(int id, [FromBody] Customer customer)
        {
            this.ParseResource();
            return this._customerService.Update(this.Resource, customer);
        }

        // PATCH: {resource}/Customers({id})
        [UserHasResourceAccess, UserHasPermission("Modify")]
        [EnableQuery, HttpPatch, ODataRoute("({id})")]
        public Customer PartialUpdate(int id, [FromBody] Delta<Customer> customer)
        {
            this.ParseResource();
            return this._customerService.PartialUpdate(this.Resource, id, customer);
        }

        // PUT: {resource}/Customers({id})
        [UserHasResourceAccess, UserHasPermission("Modify")]
        [EnableQuery, HttpPut, ODataRoute("({customerId})/Offers({offerId})")]
        public Customer AddOffer(int customerId, int offerId)
        {
            this.ParseResource();
            return this._customerService.AddOffer(this.Resource, customerId, offerId);
        }

        // DELETE: {resource}/Customers({id})
        [UserHasResourceAccess, UserHasPermission("Remove")]
        [EnableQuery, HttpDelete, ODataRoute("({id})")]
        public void Delete(int id)
        {
            this.ParseResource();
            this._customerService.Delete(this.Resource, id);
            this.ODataDeleted(); // Set response headers
        }
    }
}