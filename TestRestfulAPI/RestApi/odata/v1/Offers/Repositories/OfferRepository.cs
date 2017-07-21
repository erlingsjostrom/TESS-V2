using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.OData;
using TestRestfulAPI.Infrastructure.Exceptions;
using TestRestfulAPI.Infrastructure.Repositories;
using TestRestfulAPI.RestApi.odata.v1.Contents.Entities;
using TestRestfulAPI.RestApi.odata.v1.Customers.Entities;
using TestRestfulAPI.RestApi.odata.v1.Offers.Entities;
using TestRestfulAPI.RestApi.odata.v1.Offers.Exceptions;
using ResourceContext = TestRestfulAPI.Infrastructure.Database.ResourceContext;

namespace TestRestfulAPI.RestApi.odata.v1.Offers.Repositories
{
    public class OfferRepository : BaseRepository<Offer>, IRepository<Offer, int, string>
    {
        public OfferRepository(IEnumerable<ResourceContext> resourceContexts) : base(resourceContexts)
        {
            
        }

        public IEnumerable<IQueryable<Offer>> All()
        {
            var results = new List<IQueryable<Offer>>();
            foreach (var resourceContext in this.ResourceContexts)
            {
                results.Add(resourceContext.Context.Set<Offer>());
            }
            return results;
        }
        public ResultSet<IQueryable<Offer>> AllWithResourceContext()
        {
            var results = new ResultSet<IQueryable<Offer>>("Offers");
            foreach (var resourceContext in this.ResourceContexts)
            {
                results.Add(resourceContext.Name, resourceContext.Context.Set<Offer>());
            }
            return results;
        }

        public IQueryable<Offer> All(string resource)
        {
            var results = this.GetAndValidateResource(resource);
            return results.Context.Set<Offer>();
        }

        public Offer Get(string resource, int id)
        {
            var results = GetAndValidateResource(resource);
            var offer = results
                .Context.Set<Offer>()
                .FirstOrDefault(o => o.Id == id);
            if (offer == null)
            {
                throw new OfferDoesNotExistException("Offer with ID " + id + " does not exist");
            }

            return offer;
        }
        public ResultSet<Offer> GetWithResourceContext(string resource, int id)
        {
            var results = GetAndValidateResource(resource);

            var offer = results
                .Context.Set<Offer>()
                .FirstOrDefault(o => o.Id == id);
            if (offer == null)
            {
                throw new OfferDoesNotExistException("Offer with ID " + id + " does not exist.");
            }
            var result = new ResultSet<Offer>("Offers");
            result.Add(resource, offer);
            return result;
        }

        public Offer Create(string resource, Offer entity)
        {
            var results = GetAndValidateResource(resource);

            results.Context.Set<Offer>().Add(entity);
            this.SetTimeStamps(ref entity);
            results.Context.SaveChanges();

            return entity;
        }
        public Offer CreateCustomerOffer(string resource, Offer offer, Customer customer)
        {
            var results = GetAndValidateResource(resource);
            customer.Offers.Add(offer);
            results.Context.SaveChanges();
            return offer;
        }

        public Offer Update(string resource, Offer entity)
        {
            var results = GetAndValidateResource(resource);

            var dbEntry = this.Get(resource, entity.Id);

            results.Context.Entry(dbEntry).CurrentValues.SetValues(entity);
            results.Context.Entry(dbEntry).Property("CreatedAt").IsModified = false;

            results.Context.SaveChanges();
            return dbEntry;
        }

        public Offer PartialUpdate(string resource, int id, Delta<Offer> entity)
        {
            var results = GetAndValidateResource(resource);
            var dbEntry = this.Get(resource, id);
            entity.Patch(dbEntry);
            results.Context.Entry(dbEntry).Property("CreatedAt").IsModified = false;

            results.Context.SaveChanges();

            return dbEntry;
        }

        public void Delete(string resource, int id)
        {
            var results = GetAndValidateResource(resource);

            var dbEntry = this.Get(resource, id);

            results.Context.Set<Offer>().Remove(dbEntry);
            results.Context.SaveChanges();
        }

        public Offer AddContent(string resource, int offerId, Content content)
        {
            var results = GetAndValidateResource(resource);
            var offer = Get(resource, offerId);
            offer.Contents.Add(content);
            results.Context.SaveChanges();
            return offer;
        }
        public Offer RemoveContent(string resource, int offerId, Content content)
        {
            var results = GetAndValidateResource(resource);
            var offer = Get(resource, offerId);
            offer.Contents.Remove(content);
            results.Context.SaveChanges();
            return offer;
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
        private void SetTimeStamps(ref Offer entity)
        {
            if (entity.CreatedAt == new DateTime())
            {
                entity.CreatedAt = DateTime.Now;
            }
            entity.UpdatedAt = DateTime.Now;
        }
    }
}