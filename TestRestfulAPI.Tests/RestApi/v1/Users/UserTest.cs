using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestRestfulAPI.Infrastructure.Repositories;
using TestRestfulAPI.RestApi.v1.Users.Context;
using TestRestfulAPI.RestApi.v1.Users.Repositories;

namespace TestRestfulAPI.Tests.RestApi.v1.Users
{
    [TestClass]
    public class UserTest
    {
        private UserRepository userRepository;

        public UserTest()
        {
            var connString = ConfigurationManager.ConnectionStrings["UserEntities"].ConnectionString;

            this.userRepository = new UserRepository(
                new List<UserEntities>()
                {
                    new UserEntities()
                }
            );
        }
        [TestMethod]
        public void GetAll_Users()
        {
            var results = this.userRepository.Get().First().Value.ToList();
            

            Assert.IsNotNull(results);
        }
    }
}
