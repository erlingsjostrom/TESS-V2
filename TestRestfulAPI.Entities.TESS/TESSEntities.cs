using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRestfulAPI.Entities.TESS
{
    public class TESSEntities : DbContext
    {
        public TESSEntities() : base("name=TESSEntities")
        {
            
        }

        public TESSEntities(EntityConnection entityConnection) : base(entityConnection, true)
        {
            
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Customer> Customers { get; set; }

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
    }
}
