using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRestfulAPI.Entities.TESS
{
    class User : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string WindowsUser { get; set; }

    }
}
