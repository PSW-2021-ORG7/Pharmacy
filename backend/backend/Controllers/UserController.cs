using AutoMapper;
using backend.Model;
using backend.DTO;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly UserService _userService;

        public UserController(IConfiguration configuration, IMapper mapper, UserService userService)
        {
            _configuration = configuration;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpGet("get-all-users")]
        public IActionResult GetUsers()
        {
            return Ok(_userService.GetAll());
        }

        //[Authorize]
        [HttpGet("get-user-by-username/{username}")]
        public IActionResult GetUserByUsername(string username)
        {
            return Ok(_userService.GetUserByUsername(username));
        }


        [HttpPost("register-client")]
        public IActionResult RegisterClient([FromBody] UserRegistrationDTO userDTO)
        {
            User newUser  = _mapper.Map<User>(userDTO);
            newUser.Role = Model.User.UserRole.Client;

           if(_userService.RegisterUser(newUser))
             return Ok(new { message = "Success" });

            return BadRequest(new { message = "Username already exist" });
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginRequestDTO data)
        {
            bool isValid = _userService.IsValidUserLoginData(data);
            if (isValid)
            {
                var tokenString = GenerateJwtToken(data.Username);
                return Ok(new { Token = tokenString, Message = "Success" });
            }
            return BadRequest("Please pass the valid username and password");
        }

        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet(nameof(GetResult))]
        public IActionResult GetResult()
        {
            return Ok("API Validated");
        }

        /// <summary>
        /// Generate JWT Token after successful login.
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        private string GenerateJwtToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", username) }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        /*
        [HttpPost("login")]
        public IActionResult Login(UserLoginRequestDTO loginDTO)
        {

            UserLoginResponseDTO response = _userService.Authenticate(loginDTO);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }
        */
    }
}
