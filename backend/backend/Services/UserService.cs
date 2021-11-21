using backend.DTO;
using backend.Helpers;
using backend.Model;
using backend.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace backend.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly AppSettings _appSettings;

        public UserService(IUserRepository userRepository, IOptions<AppSettings> appSettings)
        {
            _userRepository = userRepository;
            _appSettings = appSettings.Value;
        }

        public List<User> GetAll()
        {
            return _userRepository.GetAll();

        }
        public User GetUserByUsername (string username)
        {
            return _userRepository.GetByUsername(username);

        }

        public UserLoginRequestDTO GetUserCredentials (string username)
        {
            UserLoginRequestDTO credentials = new UserLoginRequestDTO();
            credentials.Username = username;
            credentials.Password = GetUserByUsername(username).Password;
            return credentials;
        }

        public bool RegisterUser(User newUser)
        {
            if (_userRepository.Save(newUser))
                return true;

            return false;
        }

        public bool IsValidUserLoginData(UserLoginRequestDTO userDTO)
        {
            User user = _userRepository.GetAll().SingleOrDefault(u => u.Username == userDTO.Username && u.Password == userDTO.Password);

            if (user == null)
                return false;

            return true;

        }

    }
}
