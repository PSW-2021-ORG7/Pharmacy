using backend.DTO;
using backend.Model;
using backend.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace backend.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<User> GetAll()
        {
            return _userRepository.GetAll();

        }
        public User GetUserByUsername (string username)
        {
            return _userRepository.GetByUsername(username);

        }

        public bool RegisterUser(User newUser)
        {
            /*
            if (_userRepository.Save(newUser))
                return true;

            return false;
            */

            if (_userRepository.GetAll().Any(u => u.Username == newUser.Username))
                return false;

            _userRepository.Save(newUser);
            return true;
        }

        public User GetUserByLoginCredentials(UserLoginRequestDTO userDTO)
        {
            User user = _userRepository.GetAll().SingleOrDefault(u => u.Username == userDTO.Username && u.Password == userDTO.Password);
            return user;
        }
    }
}
