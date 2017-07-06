using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRestfulAPI.Entities.User
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
