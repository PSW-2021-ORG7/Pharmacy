using System;

namespace backend.DTO
{
    public class UserLoginRequestDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public UserLoginRequestDTO() {}
    }
}
