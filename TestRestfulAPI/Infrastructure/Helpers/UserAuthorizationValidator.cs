using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestRestfulAPI.Entities.User;

namespace TestRestfulAPI.Infrastructure.Helpers
{
    public class UserAuthorizationValidator
    {
        public bool UserHasRoles(User user, string[] claimedRoles)
        {
            var userRoles = user.Roles.Select(r => r.Name).ToArray();
            var rolesAreEqual = userRoles.OrderBy(a => a).SequenceEqual(claimedRoles.OrderBy(a => a));
            return rolesAreEqual;
        }
    }
}