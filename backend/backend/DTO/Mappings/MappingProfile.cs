using AutoMapper;
using backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DTO.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegistrationDTO, User>();
            CreateMap<User, UserRegistrationDTO>();
        }
    }
}
