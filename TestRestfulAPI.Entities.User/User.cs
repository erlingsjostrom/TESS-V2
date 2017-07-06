using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRestfulAPI.Entities.TESS
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
