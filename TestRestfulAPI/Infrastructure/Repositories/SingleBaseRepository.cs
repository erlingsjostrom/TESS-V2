using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TestRestfulAPI.Infrastructure.Database;

namespace TestRestfulAPI.Infrastructure.Repositories
{
    public class SingleBaseRepository<T> : IDisposable where T : class 
    {
        protected ResourceContext ResourceContext;

        public SingleBaseRepository(ResourceContext resourceContext)
        {
            this.ResourceContext = resourceContext;
        }

        public void Dispose()
        {
            this.ResourceContext.Context.Dispose();
        }
    }
}