using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRestfulAPI.Entities.User
{
    public class NamedEntityDto : BaseEntityDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
