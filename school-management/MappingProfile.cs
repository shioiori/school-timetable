using AutoMapper;
using school_management.DTOs;
using school_management.Models;

namespace school_management
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Shift, ShiftDTO>();
            CreateMap<ShiftDTO, Shift>();
        }
    }
}
