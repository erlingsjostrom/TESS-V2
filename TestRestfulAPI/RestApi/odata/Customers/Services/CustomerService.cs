using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.OData;
using TestRestfulAPI.Entities.TESS;
using TestRestfulAPI.Infrastructure.Database;
using TestRestfulAPI.Infrastructure.Repositories;
using TestRestfulAPI.RestApi.odata.Customers.Repositories;
using TestRestfulAPI.RestApi.odata.Users.Services;
using ResourceContext = TestRestfulAPI.Infrastructure.Database.ResourceContext;


namespace TestRestfulAPI.RestApi.odata.Customers.Services
{
    public class CustomerService
    {
        private readonly UserService _userService;

        public CustomerService(UserService userService)
        {
            this._userService = userService;
        }

        public IEnumerable<IQueryable<Customer>> All()
        {
            var customerRepository = this.GetCustomerRepository();
            return customerRepository.All();
        }

        public ResultSet<IQueryable<Customer>> AllWithResourceContext()
        {
            var customerRepository = this.GetCustomerRepository();
            return customerRepository.AllWithResourceContext();
        }

        public IQueryable<Customer> All(string resource)
        {
            var customerRepository = this.GetCustomerRepository();
            return customerRepository.All(resource);
        }

        public Customer Get(string resource, int id)
        {
            var customerRepository = this.GetCustomerRepository();
            return customerRepository.Get(resource, id);
        }
        public Customer Create(string resource, Customer customer)
        {
            var customerRepository = this.GetCustomerRepository();
            return customerRepository.Create(resource, customer);
        }
        public Customer Update(string resource, Customer customer)
        {
            var customerRepository = this.GetCustomerRepository();
            return customerRepository.Update(resource, customer);
        }

        public Customer PartialUpdate(string resource, int id, Delta<Customer> customer)
        {
            var customerRepository = this.GetCustomerRepository();
            return customerRepository.PartialUpdate(resource, id, customer);
        }

        public void Delete(string resource, int id)
        {
            var customerRepository = this.GetCustomerRepository();
            customerRepository.Delete(resource, id);
        }

        private CustomerRepository GetCustomerRepository()
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

            return new CustomerRepository(resourceContexts);
        }
    }
}