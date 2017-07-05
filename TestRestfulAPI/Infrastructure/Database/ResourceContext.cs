using System;
using System.Data.Entity;

namespace TestRestfulAPI.Infrastructure.Database
{
    public class ResourceContext
    {
        public string Name { get; }
        public DbContext Context { get; set; }

        public Type ContextType { get; set; }

        public ResourceContext(string name, DbContext context, Type contextType)
        {
            this.Name = name;
            this.Context = context;
            this.ContextType = contextType;
        }

        public void Refresh()
        {
            var dbName = this.Context.Database.Connection.Database;
            this.Context.Dispose();
            this.Context = DbContextFactory.Get(dbName, this.ContextType);
        }
    }
}