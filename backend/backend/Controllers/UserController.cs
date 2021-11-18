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
