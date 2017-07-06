using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRestfulAPI.Entities.User
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
