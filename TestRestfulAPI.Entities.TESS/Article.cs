using System.ComponentModel.DataAnnotations.Schema;
using TestRestfulAPI.Entities.User;

namespace TestRestfulAPI.Entities.TESS
{
    public class Article : NamedEntity
    {
        [Index(IsUnique = true)]
        public int ArticleNumber { get; set; }
    }
}
