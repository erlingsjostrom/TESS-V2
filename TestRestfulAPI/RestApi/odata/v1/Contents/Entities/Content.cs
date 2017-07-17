using System.Collections.Generic;
using TestRestfulAPI.Infrastructure.Entities;
using TestRestfulAPI.RestApi.odata.v1.Offers.Entities;

namespace TestRestfulAPI.RestApi.odata.v1.Contents.Entities
{
    public class Content : BaseEntity
    {
        public int Order { get; set; }
        public string Type { get; set; }

        public virtual Offer Offer { get; set; }
        public virtual ICollection<TextItem> TextItems { get; set; }
        public virtual Template Template { get; set; }
    }
}