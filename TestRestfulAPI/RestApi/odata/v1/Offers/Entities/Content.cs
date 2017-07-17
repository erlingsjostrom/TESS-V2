using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestRestfulAPI.Infrastructure.Entities;

namespace TestRestfulAPI.RestApi.odata.v1.Offers.Entities
{
    public class Content : BaseEntity
    {
        public int Order { get; set; }
        public string Type { get; set; }

        public virtual Offer Offer { get; set; }
        public virtual ICollection<TextItem> TextItems { get; set; }
    }
}