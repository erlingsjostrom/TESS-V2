using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestRestfulAPI.Infrastructure.Entities;

namespace TestRestfulAPI.RestApi.odata.v1.Offers.Entities
{
    public class TextItem : BaseEntity
    {
        public string Data { get; set; }
        public virtual Content Content { get; set; }
    }
}