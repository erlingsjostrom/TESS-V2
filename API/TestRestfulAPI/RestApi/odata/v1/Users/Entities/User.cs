using System.Collections.Generic;
using TestRestfulAPI.Infrastructure.Entities;

namespace TestRestfulAPI.RestApi.odata.v1.Users.Entities
{
    public class User : BaseEntity
    {
        public User()
        {
            this.Roles = new HashSet<Role>();
            this.Resources = new HashSet<Resource>();
        }


        // Properties
        public string Name { get; set; }
        public string WindowsUser { get; set; }

        // Relations
        public virtual ICollection<Role> Roles { get; set; }

        public virtual ICollection<Resource> Resources { get; set; }
    }
}
