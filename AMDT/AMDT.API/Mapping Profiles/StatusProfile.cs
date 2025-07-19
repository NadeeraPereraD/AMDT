using AutoMapper;
using AMDT.API.Models.DTOs;
using AMDT.API.Models.Entities;

namespace AMDT.API.Mapping_Profiles
{
    public class StatusProfile : Profile
    {
        public StatusProfile() 
        {
            CreateMap<Status, StatusDto>()
                .ForMember(dest => dest.StatusID, opt => opt.MapFrom(src => src.StatusId));
        }
    }
}
