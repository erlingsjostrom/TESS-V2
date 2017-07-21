using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Threading.Tasks;
using TestRestfulAPI.Infrastructure.Entities;
using TestRestfulAPI.RestApi.odata.v1.Articles.Entities;
using TestRestfulAPI.RestApi.odata.v1.Contents.Entities;
using TestRestfulAPI.RestApi.odata.v1.Customers.Entities;
using TestRestfulAPI.RestApi.odata.v1.Offers.Entities;

namespace TestRestfulAPI.Infrastructure.Contexts
{
    public class TESSEntities : DbContext
    {
        public TESSEntities() : base("name=TESSEntities")
        {
            
        }

        public TESSEntities(DbConnection entityConnection) : base(entityConnection, true)
        {
            
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<TextItem> TextItems { get; set; }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }
        public override async Task<int> SaveChangesAsync()
        {
            AddTimestamps();
            return await base.SaveChangesAsync();
        }
        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));


            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedAt = DateTime.Now;
                }

                ((BaseEntity)entity.Entity).UpdatedAt = DateTime.Now;
            }
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Offer>()
                .HasMany(o => o.Contents)
                .WithOptional()
                .WillCascadeOnDelete();
            modelBuilder.Entity<Content>()
                .HasMany(c => c.TextItems)
                .WithOptional()
                .WillCascadeOnDelete();
            modelBuilder.Entity<Content>()
                .HasMany(c => c.Articles)
                .WithOptional()
                .WillCascadeOnDelete();
            modelBuilder.Entity<Template>()
                .HasMany(c => c.Contents)
                .WithOptional()
                .WillCascadeOnDelete();
            base.OnModelCreating(modelBuilder);
        }
    }
}
