using System.Linq;
using System.Web;
using System.Web.OData;
using TestRestfulAPI.Infrastructure.Contexts;
using TestRestfulAPI.Infrastructure.Database;
using TestRestfulAPI.Infrastructure.Services;
using TestRestfulAPI.RestApi.odata.v1.Contents.Repositories;
using TestRestfulAPI.RestApi.odata.v1.Customers.Repositories;
using TestRestfulAPI.RestApi.odata.v1.Offers.Entities;
using TestRestfulAPI.RestApi.odata.v1.Offers.Repositories;
using TestRestfulAPI.RestApi.odata.v1.Users.Services;
using ResourceContext = TestRestfulAPI.Infrastructure.Database.ResourceContext;

namespace TestRestfulAPI.RestApi.odata.v1.Offers.Services
{
    public class OfferService : IService<Offer, int, string>
    {
        private readonly UserService _userService;
        private OfferRepository _offerRepository;
        private CustomerRepository _customerRepository;
        private ContentRepository _contentRepository;

        public OfferService(UserService userService)
        {
            this._userService = userService;
        }

        public IQueryable<Offer> All(string resource)
        {
            this.InitRepository();
            return _offerRepository.All(resource);
        }

        public Offer Get(string resource, int id)
        {
            this.InitRepository();
            return _offerRepository.Get(resource, id);
        }

        public Offer Create(string resource, Offer offer)
        {
            this.InitRepository();
            return _offerRepository.Create(resource, offer);
        }

        public Offer CreateCustomerOffer(string resource, Offer offer, int customerId)
        {
            this.InitRepository();
            var customer = this._customerRepository.Get(resource, customerId);
            return _offerRepository.CreateCustomerOffer(resource, offer, customer);
        }

        public Offer Update(string resource, Offer offer)
        {
            this.InitRepository();
            return _offerRepository.Update(resource, offer);
        }

        public Offer PartialUpdate(string resource, int id, Delta<Offer> offer)
        {
            this.InitRepository();
            return _offerRepository.PartialUpdate(resource, id, offer);
        }

        public void Delete(string resource, int id)
        {
            this.InitRepository();
            _offerRepository.Delete(resource, id);
        }

        public Offer AddContent(string resource, int offerId, int contentId)
        {
            this.InitRepository();
            var content = this._contentRepository.Get(resource, contentId);
            return _offerRepository.AddContent(resource, offerId, content);
        }
        public Offer RemoveContent(string resource, int offerId, int contentId)
        {
            this.InitRepository();
            var content = this._contentRepository.Get(resource, contentId);
            return _offerRepository.RemoveContent(resource, offerId, content);
        }

        private void InitRepository()
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

            this._offerRepository = new OfferRepository(resourceContexts);
            this._customerRepository = new CustomerRepository(resourceContexts);
            this._contentRepository = new ContentRepository(resourceContexts);
        }
    }
}