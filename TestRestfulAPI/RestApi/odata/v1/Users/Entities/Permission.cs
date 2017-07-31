using System.Collections.Generic;
using TestRestfulAPI.Infrastructure.Entities;

namespace TestRestfulAPI.RestApi.odata.v1.Users.Entities
{
    public class Permission : NamedEntity
    {
        public Permission()
        {
            this.Roles = new HashSet<Role>();
        }

        // Properties

        // Relations
        public virtual ICollection<Role> Roles { get; set; }
    }
}
