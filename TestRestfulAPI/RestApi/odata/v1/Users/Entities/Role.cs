using System.Collections.Generic;
using TestRestfulAPI.Infrastructure.Entities;

namespace TestRestfulAPI.RestApi.odata.v1.Users.Entities
{
    public class Role : NamedEntity
    {
        public Role()
        {
            this.Permissions = new HashSet<Permission>();
        }

        // Properties

        // Relations
        public virtual ICollection<Permission> Permissions { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
