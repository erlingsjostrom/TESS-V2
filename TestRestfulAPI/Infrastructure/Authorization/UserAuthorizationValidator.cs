using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestRestfulAPI.RestApi.odata.v1.Users.Entities;

namespace TestRestfulAPI.Infrastructure.Authorization
{
    /// <summary>
    /// Provides validation of User Roles and Permissions. 
    /// NOTICE: The Admin Role will override all rules.
    /// </summary>
    public class UserAuthorizationValidator
    {
        private readonly User _user;
        public UserAuthorizationValidator()
        {
            var userName = HttpContext.Current.User.Identity.Name;
            this._user = GlobalServices.UserService.GetByWindowsIdentityName(userName);
        }

        /// <summary>
        /// Validate that User has the requiredRoles
        /// </summary>
        /// <param name="requiredRoles">roles to check</param>
        /// <returns>true if the User has the roles</returns>
        public bool UserHasRoles(string[] requiredRoles)
        {        
            var userRoleNames = this.GetUserRoleNames().ToArray();

            // check if requiredRoles is a subset of userRoles
            var userHasRoles = !requiredRoles.Except(userRoleNames).Any();
    
            return this.CheckForAdminOverride() || userHasRoles;
        }

        /// <summary>
        /// Validate that User has the requierdPermissions
        /// </summary>
        /// <param name="requierdPermissions">permissions to check</param>
        /// <returns>true if the User has the required permissions</returns>
        public bool UserHasPermission(string[] requierdPermissions)
        {
            this.CheckForAdminOverride();

            var userPermissions = new List<string>();
            // Fetch all permissions, from UserRoles, distinct
            this.GetUserRoles().ToList()
                .ForEach(r => 
                userPermissions.AddRange(
                    r.Permissions
                    .GroupBy(p => p.Name)
                    .Select(g => 
                        g.First().Name
                    ).ToList()
                ));

            // check if requierdPermissions is a subset of userPermissions
            var userHasPermission = !requierdPermissions.Except(userPermissions.ToArray()).Any();
            return this.CheckForAdminOverride() || userHasPermission;
        }

        /// <summary>
        /// Validate that User has the requestedResources
        /// </summary>
        /// <param name="requestedResources">resources to check</param>
        /// <returns>true if the user has the required resources</returns>
        public bool UserHasResourceAccess(string[] requestedResources)
        {
            var resouces = this._user.Resources.Select(r => r.Name).ToArray();
            // check if requiredResources is a subset of requestedResources
            var hasResources = !requestedResources.Except(resouces).Any();
            return hasResources;
        }

        private IEnumerable<Role> GetUserRoles()
        {
            return this._user.Roles;
        }

        private IEnumerable<string> GetUserRoleNames()
        {
            return this.GetUserRoles().Select(r => r.Name);
        }

        private bool CheckForAdminOverride()
        {
            return this.GetUserRoles().Any(r => r.Name == "Admin");
        }
    }
}