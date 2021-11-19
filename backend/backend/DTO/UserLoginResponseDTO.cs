using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DTO
{
    public class UserLoginResponseDTO
    {
        public Model.User User { get; set; }
        public string Token { get; set; }

        public UserLoginResponseDTO(Model.User user, string token)
        {
            User = user;
            Token = token;
        }
    }
}
