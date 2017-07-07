using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TestRestfulAPI.Entities.TESS;
using TestRestfulAPI.Infrastructure.Authorization.Attributes;
using TestRestfulAPI.Infrastructure.Controllers;
using TestRestfulAPI.Infrastructure.Exceptions;
using TestRestfulAPI.RestApi.v1.Articles.Controllers;
using TestRestfulAPI.RestApi.v1.Customers.Services;

namespace TestRestfulAPI.RestApi.v1.Customers.Controllers
{

    [RoutePrefix("api/v1")]
    public class CustomerController : ApiJsonController
    {
        private readonly CustomerService _customerService = GlobalServices.CustomerService;

        // GET: customers
        //[UserHasRole("SalesPerson")]
        [HttpGet, Route("customers")]
        public IHttpActionResult Customers()
        {
            return Json(this._customerService.AllWithResourceContext(), "Collection");
        }

        // GET: customers/{INVALID}
        [HttpGet, Route("customers/{INVALID}")]
        public IHttpActionResult Customers(object invalid)
        {
            throw new InvalidEndpointException();
        }

        // GET: {resource}/customers
        [UserHasResourceAccess]
        [UserHasPermission("Read")]
        [HttpGet, Route("{resource}/customers")]
        public IHttpActionResult Customers(string resource)
        {
            return Json(this._customerService.All(resource));
        }

        // GET: {resource}/customers/{id}
        [UserHasResourceAccess]
        [UserHasPermission("Read")]
        [HttpGet, Route("{resource}/customers/{id}")]
        public IHttpActionResult Customers(string resource, int id)
        {
            return Json(this._customerService.Get(resource, id));
        }

        // POST: {resource}/customers
        [UserHasResourceAccess]
        [UserHasPermission("Create")]
        [HttpPost, Route("{resource}/customers")]
        public IHttpActionResult Create(string resource, Customer customer)
        {
            var newCustomer = this._customerService.Create(resource, customer);
            return JsonCreated(newCustomer, newCustomer.Id);
        }
        
        // PUT: {resource}/customers
        [UserHasResourceAccess]
        [UserHasPermission("Modify")]
        public IHttpActionResult Update(string resource, int id, Customer customer)
        {
            var updatedCustomer = this._customerService.Update(resource, customer);
            return Json(updatedCustomer);
        }
    }
}