using System.ComponentModel.DataAnnotations.Schema;
using TestRestfulAPI.Infrastructure.Entities;

namespace TestRestfulAPI.RestApi.odata.v1.Articles.Entities
{
    public class Article : NamedEntity
    {
        [Index(IsUnique = true)]
        public int ArticleNumber { get; set; }
    }

    public class ArticleDto : NamedEntityDto
    {

    }
}
