using AutoMapper;
using backend.DTO;
using backend.Model;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private UserService _userService;

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

        [HttpGet("get-user-by-username/{username}")]
        public IActionResult GetUserByUsername(string username)
        {
            return Ok(_userService.GetUserByUsername(username));
        }


        [HttpPost("register-client")]
        public IActionResult RegisterClient([FromBody] UserRegistrationDTO userDTO)
        {
            User newUser = new User();
            newUser = _mapper.Map<UserRegistrationDTO, User>(userDTO, newUser);
            newUser.Role = Model.User.UserRole.Client;
            _userService.RegisterUser(newUser);

            return Ok();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDTO loginDTO)
        {
            User user = new User();
            user = _mapper.Map<UserLoginDTO, User>(loginDTO, user);
            //authentication?

            return Ok();
        }

    }
}
