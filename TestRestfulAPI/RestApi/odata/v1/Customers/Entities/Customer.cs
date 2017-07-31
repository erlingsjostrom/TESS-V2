using System.Collections.Generic;
using TestRestfulAPI.Infrastructure.Entities;
using TestRestfulAPI.RestApi.odata.v1.Offers.Entities;

namespace TestRestfulAPI.RestApi.odata.v1.Customers.Entities
{
    public class Customer : BaseEntity
    {
        public Customer()
        {
            Offers = new HashSet<Offer>();
        }
        public string Name { get; set; }
        public string Type { get; set; }
        public string WebAddress { get; set; }
        public string CorporateIdentityNumber { get; set; }
        public string EntityType { get; set; }
        public virtual ICollection<Offer> Offers { get; set; }

    }

    public class CustomerDto : BaseEntityDto
    {
        public string Name { get; set; }
        public string CustomerType { get; set; }
        public string CorporateIdentityNumber { get; set; }
    }
}