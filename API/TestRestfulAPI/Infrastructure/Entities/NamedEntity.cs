using System.ComponentModel.DataAnnotations;

namespace TestRestfulAPI.Infrastructure.Entities
{
    public abstract class NamedEntity : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
