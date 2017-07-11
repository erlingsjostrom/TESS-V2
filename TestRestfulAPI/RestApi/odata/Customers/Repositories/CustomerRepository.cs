using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestRestfulAPI.Entities.TESS;
using TestRestfulAPI.Infrastructure.Database;
using TestRestfulAPI.Infrastructure.Exceptions;
using TestRestfulAPI.Infrastructure.Repositories;
using TestRestfulAPI.RestApi.odata.Customers.Exceptions;

namespace TestRestfulAPI.RestApi.odata.Customers.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, IRepository<Customer, int, string>
    {
        public CustomerRepository(IEnumerable<ResourceContext> resourceContexts) : base(resourceContexts)
        {
        }

        public IEnumerable<IQueryable<Customer>> All()
        {
            var results = new List<IQueryable<Customer>>();
            foreach (var resourceContext in this.ResourceContexts)
            {
                results.Add(resourceContext.Context.Set<Customer>());
            }
            return results;
        }
        public ResultSet<IQueryable<Customer>> AllWithResourceContext()
        {
            var results = new ResultSet<IQueryable<Customer>>("Customers");
            foreach (var resourceContext in this.ResourceContexts)
            {
                results.Add(resourceContext.Name, resourceContext.Context.Set<Customer>());
            }
            return results;
        }
        public IQueryable<Customer> All(string resource)
        {
            var results = this.GetAndValidateResource(resource);
            return results.Context.Set<Customer>();
        }
        public Customer Get(string resource, int id)
        {
            var results = GetAndValidateResource(resource);
            var customer = results
                .Context.Set<Customer>()
                .FirstOrDefault(a => a.Id == id);
            if (customer == null)
            {
                throw new CustomerDoesNotExistException("Customer with ID " + id + " does not exist");
            }

            return customer;
        }
        public ResultSet<Customer> GetWithResourceContext(string resource, int id)
        {
            var results = GetAndValidateResource(resource);

            var customer = results
                .Context.Set<Customer>()
                .FirstOrDefault(a => a.Id == id);
            if (customer == null)
            {
                throw new CustomerDoesNotExistException("Customer with ID " + id + " does not exist.");
            }
            var result = new ResultSet<Customer>("Customers");
            result.Add(resource, customer);
            return result;
        }
        public Customer Create(string resource, Customer entity)
        {
            var results = GetAndValidateResource(resource);

            var customer = results
                .Context.Set<Customer>()
                .FirstOrDefault(c => c.CorporateIdentityNumber == entity.CorporateIdentityNumber);
            if (customer != null)
            {
                throw new CustomerAlreadyExistException("Customer with Corporate Identity Number does already exist.");
            }
            results.Context.Set<Customer>().Add(entity);
            this.SetTimeStamps(ref entity);
            results.Context.SaveChanges();

            return entity;
        }
        public Customer Update(string resource, Customer entity)
        {
            var results = GetAndValidateResource(resource);

            results.Context.Set<Customer>().Attach(entity);
            this.SetTimeStamps(ref entity);
            results.Context.SaveChanges();

            return entity;
        }
        private void SetTimeStamps(ref Customer entity)
        {
            if (entity.CreatedAt == new DateTime())
            {
                entity.CreatedAt = DateTime.Now;
            }
            entity.UpdatedAt = DateTime.Now;
        }
        private ResourceContext GetAndValidateResource(string resource)
        {
            var results = this.ResourceContexts.FirstOrDefault(c => c.Name == resource);
            if (results == null)
            {
                throw new InvalidDbContextTypeException("Resource not found");
            }
            return results;
        }
    }
}