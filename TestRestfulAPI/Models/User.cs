using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestRestfulAPI.Models
{
    public class User
    {
        public int Id { get; }

        public string Name { get; }

        public User(int id)
        {
            this.Id = id;
            if (id == 1)
            {
                this.Name = "Anton";
            }
            else if (id == 2)
            {
                this.Name = "Erik";
            }
        }

        public static ICollection<User> all()
        {
            return new List<User>()
            {
                new User(1),
                new User(2)
            };
        }
    }
}