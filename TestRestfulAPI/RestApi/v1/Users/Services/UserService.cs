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
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public User GetByWindowsIdentityName(string windowsIdentityName)
        {
            return this._userRepository.GetByWindowsIdentityName(windowsIdentityName);
        }

        public IQueryable<User> All()
        {
            return this._userRepository.All();
        }

        public User Get(int id)
        {
            return this._userRepository.Get(id);
        }

        public User Create(User user)
        {
            return this._userRepository.Create(user);
        }

        public User Update(User user)
        {
            return this._userRepository.Update(user);
        }
    }
}