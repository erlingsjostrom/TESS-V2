using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Routing;
using AutoMapper;
using Microsoft.Web.Http;
using TestRestfulAPI.Entities.TESS;
using TestRestfulAPI.Infrastructure.Authorization.Attributes;
using TestRestfulAPI.Infrastructure.Controllers;
using TestRestfulAPI.Infrastructure.Exceptions;
using TestRestfulAPI.RestApi.odata.Articles.Controllers;
using TestRestfulAPI.RestApi.odata.Customers.Services;
using TestRestfulAPI.RestApi.odata.Controllers;

namespace TestRestfulAPI.RestApi.odata.Customers.Controllers
{
    [ApiVersion("1.0")]
    [ODataRoutePrefix("Customers")]
    public class CustomerController : ResourceODataController, ICrudController<Customer>
    {
        private readonly CustomerService _customerService = GlobalServices.CustomerService;

        // GET: {resource}/Customer()
        [UserHasResourceAccess]
        [UserHasPermission("Read")]
        [EnableQuery, HttpGet, ODataRoute()]
        public IQueryable<Customer> Get()
        {
            this.ParseResource();
            return this._customerService.All(this.Resource);
        }

        // GET: {resource}/Customer({id})
        [UserHasResourceAccess]
        [UserHasPermission("Read")]
        [EnableQuery, HttpGet, ODataRoute("({id})")]
        public Customer Get(int id)
        {
            this.ParseResource();
            return this._customerService.Get(this.Resource, id);
        }

        // POST: {resource}/Customer()
        [UserHasResourceAccess]
        [UserHasPermission("Write")]
        [EnableQuery, HttpPost, ODataRoute("()")]
        public IHttpActionResult Create([FromBody] Customer customer)
        {
            this.ParseResource();
            return ODataCreated(this._customerService.Create(this.Resource, customer), customer.Id);
        }

        [UserHasResourceAccess]
        [UserHasPermission("Modify")]
        [EnableQuery, HttpPut, ODataRoute("({id})")]
        public Customer Update(int id, [FromBody] Customer customer)
        {
            this.ParseResource();
            return this._customerService.Update(this.Resource, customer);
        }

        [UserHasResourceAccess]
        [UserHasPermission("Modify")]
        [EnableQuery, HttpPatch, ODataRoute("({id})")]
        public Customer PartialUpdate(int id, [FromBody] Delta<Customer> customer)
        {
            this.ParseResource();
            return this._customerService.PartialUpdate(this.Resource, id, customer);
        }

        [EnableQuery, HttpDelete, ODataRoute("({id})")]
        public void Deleted(int id)
        {
            this.ParseResource();
            this._customerService.Delete(this.Resource, id);
            HttpContext.Current.Response.StatusCode = (int) HttpStatusCode.NoContent;
        }
    }
}