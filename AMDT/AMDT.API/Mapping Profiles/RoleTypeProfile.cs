using AMDT.API.Models.DTOs;
using AMDT.API.Models.Entities;
using AutoMapper;

namespace AMDT.API.Mapping_Profiles
{
    public class RoleTypeProfile : Profile
    {
        public RoleTypeProfile() 
        {
            CreateMap<RoleType, RoleTypeDto>()
                .ForMember(dest => dest.RoleID, opt => opt.MapFrom(src => src.RoleId));
        }
    }
}
