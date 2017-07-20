using System.ComponentModel.DataAnnotations.Schema;
using TestRestfulAPI.Infrastructure.Entities;
using TestRestfulAPI.RestApi.odata.v1.Contents.Entities;

namespace TestRestfulAPI.RestApi.odata.v1.Articles.Entities
{
    public class Article : NamedEntity
    {
        [Index(IsUnique = true)]
        public int ArticleNumber { get; set; }
        public virtual Content Content { get; set; }
    }

    public class ArticleDto : NamedEntityDto
    {

    }
}
