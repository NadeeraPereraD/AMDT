using AMDT.API.Models.DTOs;
using AMDT.API.Models.Entities;
using AutoMapper;

namespace AMDT.API.Mapping_Profiles
{
    public class UserDetailsProfile : Profile
    {
        public UserDetailsProfile() 
        {
            CreateMap<UserDetail, UserDetailsDto>()
                .ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.UserId));
        }
    }
}
