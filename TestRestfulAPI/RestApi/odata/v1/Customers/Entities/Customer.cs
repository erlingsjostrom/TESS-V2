using TestRestfulAPI.Infrastructure.Entities;

namespace TestRestfulAPI.RestApi.odata.v1.Customers.Entities
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public string CustomerType { get; set; }
        public string WebAddress { get; set; }
        public string CorporateIdentityNumber { get; set; }
    }

    public class CustomerDto : BaseEntityDto
    {
        public string Name { get; set; }
        public string CustomerType { get; set; }
        public string CorporateIdentityNumber { get; set; }
    }
}