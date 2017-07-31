using TestRestfulAPI.Infrastructure.Entities;

namespace TestRestfulAPI.RestApi.odata.v1.Contents.Entities
{
    public class TextItem : BaseEntity
    {
        public string Data { get; set; }
        public virtual Content Content { get; set; }
    }
}