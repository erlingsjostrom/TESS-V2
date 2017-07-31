using System.Linq;
using System.Web;
using System.Web.OData;
using TestRestfulAPI.Infrastructure.Contexts;
using TestRestfulAPI.Infrastructure.Database;
using TestRestfulAPI.Infrastructure.Services;
using TestRestfulAPI.RestApi.odata.v1.Customers.Entities;
using TestRestfulAPI.RestApi.odata.v1.Customers.Repositories;
using TestRestfulAPI.RestApi.odata.v1.Offers.Repositories;
using TestRestfulAPI.RestApi.odata.v1.Users.Services;
using ResourceContext = TestRestfulAPI.Infrastructure.Database.ResourceContext;


namespace TestRestfulAPI.RestApi.odata.v1.Customers.Services
{
    public class CustomerService : IService<Customer, int, string>
    {
        private readonly UserService _userService;
        private CustomerRepository _customerRepository;
        private OfferRepository _offerRepository;

        public CustomerService(UserService userService)
        {
            this._userService = userService;
        }

        public IQueryable<Customer> All(string resource)
        {
            this.InitRepository();
            return _customerRepository.All(resource);
        }

        public Customer Get(string resource, int id)
        {
            this.InitRepository();
            return _customerRepository.Get(resource, id);
        }
        public Customer Create(string resource, Customer customer)
        {
            this.InitRepository();
            return _customerRepository.Create(resource, customer);
        }
        public Customer Update(string resource, Customer customer)
        {
            this.InitRepository();
            return _customerRepository.Update(resource, customer);
        }

        public Customer PartialUpdate(string resource, int id, Delta<Customer> customer)
        {
            this.InitRepository();
            return _customerRepository.PartialUpdate(resource, id, customer);
        }

        public void Delete(string resource, int id)
        {
            this.InitRepository();
            _customerRepository.Delete(resource, id);
        }
        public Customer AddOffer(string resource, int customerId, int offerId)
        {
            this.InitRepository();
            var offer = this._offerRepository.Get(resource, offerId);
            return _customerRepository.AddOffer(resource, customerId, offer);
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

            this._customerRepository = new CustomerRepository(resourceContexts);
            this._offerRepository = new OfferRepository(resourceContexts);
        }
    }
}