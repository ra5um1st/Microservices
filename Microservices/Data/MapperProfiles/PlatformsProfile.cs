using AutoMapper;
using Services.Platforms.Data.DTOs;
using Services.Platforms.Data.Models;

namespace Services.Platforms.Data.MapperProfiles
{
    public class PlatformsProfile : Profile
    {
        public PlatformsProfile()
        {
            CreateMap<Platform, PlatformReadDTO>();
            CreateMap<PlatformCreateDTO, Platform>();
        }
    }
}
