using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestRestfulAPI.Entities.User;
using TestRestfulAPI.RestApi.v1.Users.Repositories;

namespace TestRestfulAPI.RestApi.v1.Users.Services
{
    public class UserService
    {
        private UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public User GetByWindowsIdentityName(string windowsIdentityName)
        {
            return this._userRepository.GetByWindowsIdentityName(windowsIdentityName);
        }
        
    }
}