using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestRestfulAPI.Infrastructure.Entities;

namespace TestRestfulAPI.RestApi.odata.v1.Contents.Entities
{
    public class Template : NamedEntity
    {
        public string EntityType { get; set; }
        public virtual ICollection<Content> Contents { get; set; }
    }
}