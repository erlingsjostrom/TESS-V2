using System;
using System.Collections.Generic;
using TestRestfulAPI.Infrastructure.Entities;
using TestRestfulAPI.RestApi.odata.v1.Contents.Entities;
using TestRestfulAPI.RestApi.odata.v1.Customers.Entities;

namespace TestRestfulAPI.RestApi.odata.v1.Offers.Entities
{
    public class Offer : BaseEntity
    {
        public Offer()
        {
            
        }
        public string Title { get; set; }
        public string Status { get; set; }
        public DateTime ValidThrough { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<Content> Contents { get; set; }
    }
}