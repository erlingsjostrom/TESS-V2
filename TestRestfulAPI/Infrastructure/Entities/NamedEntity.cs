namespace TestRestfulAPI.Infrastructure.Entities
{
    public abstract class NamedEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
