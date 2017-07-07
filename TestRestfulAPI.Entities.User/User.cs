using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestRestfulAPI.Entities.User
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
