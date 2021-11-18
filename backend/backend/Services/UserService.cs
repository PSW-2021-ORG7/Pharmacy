using backend.DTO;
using backend.Helpers;
using backend.Model;
using backend.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Nest;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace backend.Services
{
    public class UserService
    {
        private IUserRepository _userRepository;
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

        public UserLoginResponseDTO Authenticate(UserLoginRequestDTO userDTO)
        {
            User user = _userRepository.GetAll().SingleOrDefault(u => u.Username == userDTO.Username && u.Password == userDTO.Password);

            if (user == null)
                return null;

            string token = GenerateJwtToken(user);
            return new UserLoginResponseDTO(user, token);
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("UserId", user.UserId.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
