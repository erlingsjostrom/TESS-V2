using System.Collections.Generic;
using TestRestfulAPI.Infrastructure.Entities;

namespace TestRestfulAPI.RestApi.odata.v1.Users.Entities
{
    public class Resource : NamedEntity
    {
        public Resource()
        {
            this.Users = new HashSet<User>();
        }
        // Properties
        public string Location { get; set; }

        // Relations
        public virtual ICollection<User> Users { get; set; }
    }
}
