using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRestfulAPI.Entities.TESS
{
    public class Article : NamedEntity
    {
        [Index(IsUnique = true)]
        public int ArticleNumber { get; set; }
    }
}
