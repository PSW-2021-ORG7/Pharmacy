using backend.Model;
using backend.Repositories.Interfaces;
using System.Collections.Generic;

namespace backend.Services
{
    public class UserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User GetUserByUsername(string username)
        {
            return _userRepository.GetById(username);
        }

        public bool RegisterUser(User newUser)
        {
            if (_userRepository.Save(newUser))
                return true;

            return false;
        }

    }
}
