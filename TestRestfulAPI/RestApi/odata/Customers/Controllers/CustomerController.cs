using System;
using System.Collections.Generic;
using System.Linq;
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
using TestRestfulAPI.RestApi.v1.Articles.Controllers;
using TestRestfulAPI.RestApi.odata.Customers.Services;
using TestRestfulAPI.RestApi.odata.Controllers;

namespace TestRestfulAPI.RestApi.odata.Customers.Controllers
{
    [ApiVersion("1.0")]
    [ODataRoutePrefix("Customers")]
    public class CustomerController : ResourceODataController
    {
        //private readonly CustomerService _customerService = GlobalServices.CustomerService;

        [UserHasResourceAccess]
        [UserHasPermission("Read")]
        [EnableQuery, HttpGet, ODataRoute()]
        public IQueryable<Customer> Get()
        {
            this.ParseResource();
            return GlobalServices.CustomerService.All(this.Resource);
        }

        // GET: {resource}/Customers({id})
        [UserHasResourceAccess]
        [UserHasPermission("Read")]
        [EnableQuery, HttpGet, ODataRoute("({id})")]
        public Customer Get(int id)
        {
            this.ParseResource();
            return GlobalServices.CustomerService.Get(this.Resource, id);
        }

        // POST: {resource}/Customers
        [UserHasResourceAccess]
        [UserHasPermission("Create")]
        [EnableQuery, HttpPost, ODataRoute("()")]
        public Customer Create(Customer customer)
        {
            this.ParseResource();
            return GlobalServices.CustomerService.Create(this.Resource, customer);
        }
        
        // PUT: {resource}/Customers
        [UserHasResourceAccess]
        [UserHasPermission("Modify")]
        [EnableQuery, HttpPut, ODataRoute("()")]
        public Customer Update(Customer customer)
        {
            this.ParseResource();
            return GlobalServices.CustomerService.Update(this.Resource, customer);
        }
    }
}