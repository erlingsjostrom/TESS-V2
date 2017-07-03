﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TestRestfulAPI.Infrastructure.Repositories
{

    public abstract class BaseRepository<T> : IDisposable where T : class 
    {
        protected IEnumerable<ResourceContext> ResourceContexts { set; get; }

        protected BaseRepository(IEnumerable<ResourceContext> resourceContexts)
        {
            this.ResourceContexts = resourceContexts;
            this.DisableLazyLoading();
        }

        public void Dispose()
        {
            foreach (var resourceContext in this.ResourceContexts)
            {
                resourceContext.Context.Dispose();
            }
        }

        private void DisableLazyLoading()
        {
            foreach (var resourceContext in this.ResourceContexts)
            {
                resourceContext.Context.Configuration.LazyLoadingEnabled = false;
            }
        }
    }

    public class ResultSet<T> : ISerializable  where T : class
    {
        private List<string> Resources { get; }
        private List<T> Data { get; }

        public ResultSet()
        {
            this.Resources = new List<string>();
            this.Data = new List<T>();
        }

        public void Add(string resource, T data)
        {
            this.Resources.Add(resource);
            this.Data.Add(data);
        }
         
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Resources", this.Resources);
            info.AddValue("Data", this.Data);
        }
    }

    public class ResourceContext
    {
        public string Name { get; }
        public DbContext Context { get; }

        public ResourceContext(string name, DbContext context)
        {
            this.Name = name;
            this.Context = context;
        }
    }
}