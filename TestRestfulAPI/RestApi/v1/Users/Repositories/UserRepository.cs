﻿using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestRestfulAPI.Infrastructure.Exceptions;
using TestRestfulAPI.Infrastructure.Repositories;
using TestRestfulAPI.Entities.User;
using TestRestfulAPI.Infrastructure.Database;
using TestRestfulAPI.RestApi.v1.Users.Exceptions;

namespace TestRestfulAPI.RestApi.v1.Users.Repositories
{
    public class UserRepository : SingleBaseRepository<User>, ISingleRepository<User, int>
    {
        public UserRepository(ResourceContext userContext) : base(userContext)
        {}

        public IQueryable<User> All()
        {
            return this.ResourceContext.Context.Set<User>();
        }
        public User Get(int id)
        {
            var result = this.All().FirstOrDefault(u => u.Id == id);
            if (result == null)
            {
                throw new UserDoesNotExistException("User with ID " + id + " does not exist");
            }
            return result;
        }

        public User GetByWindowsIdentityName(string windowsIdentity)
        {
            this.RefreshContext();
            var user = this.All().Include("Roles").FirstOrDefault(u => u.WindowsUser == windowsIdentity);
            if (user == null)
            {
                throw new UserDoesNotExistException("User with windows identity " + windowsIdentity + " does not exist.");
            }
            
            return user;
        }

        public User Create(User entity)
        {
            ResourceContext.Context.Set<User>().Add(entity);
            this.SetTimeStamps(ref entity);
            ResourceContext.Context.SaveChanges();
           
            return entity;
        }
        public User Update(User entity)
        {
            //ResourceContext.Context.Entry(entity).State = EntityState.Modified;
            //ResourceContext.Context.Set<User>().Attach(entity);
            this.SetTimeStamps(ref entity);
            ResourceContext.Context.SaveChanges();
            ResourceContext.Context.Entry(entity).State = EntityState.Unchanged;

            return entity;
        }

        private void RefreshContext()
        {
            this.ResourceContext.Refresh();
        }
        private void SetTimeStamps(ref User entity)
        {
            if (entity.CreatedAt == new DateTime())
            {
                entity.CreatedAt = DateTime.Now;
            }
            entity.UpdatedAt = DateTime.Now;
        }
    }
}