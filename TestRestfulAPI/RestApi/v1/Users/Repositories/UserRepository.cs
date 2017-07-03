using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestRestfulAPI.Infrastructure.Repositories;
using TestRestfulAPI.Entities.User;

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
            return this.All().First(u => u.Id == id);
        }
        public User Create(User entity)
        {
            try
            {
                this.ResourceContext.Context.Set<User>().Add(entity);
                this.ResourceContext.Context.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                var message = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(e.InnerException.Message)
                };
                throw new HttpResponseException(message);
            }
           
            return entity;
        }
        public void Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}