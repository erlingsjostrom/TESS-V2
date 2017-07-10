using TestRestfulAPI.Entities.User;

namespace TestRestfulAPI.Entities.TESS
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