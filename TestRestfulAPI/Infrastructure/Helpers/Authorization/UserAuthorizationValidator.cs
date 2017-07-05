using System.Linq;
using TestRestfulAPI.Entities.User;

namespace TestRestfulAPI.Infrastructure.Helpers.Authorization
{
    public class UserAuthorizationValidator
    {
        public bool UserHasRoles(User user, string[] claimedRoles)
        {
            var userRoles = user.Roles.Select(r => r.Name).ToArray();

            // check if claimedRoles is a subset of userRoles
            var userHasRoles = !claimedRoles.Except(userRoles).Any();
            return userHasRoles;
        }
    }
}