using AnimalShelter.API.Models;
using AutoMapper;
using AnimalShelter.API.DTOs;
using System.Linq;

namespace AnimalShelter.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForInfoDto>();
            CreateMap<Animal, AnimalForListDto>()
            .ForMember(dest => dest.PhotoUrl, opt => 
                opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url));
            CreateMap<Animal, AnimalForDetailDto>()
            .ForMember(dest => dest.PhotoUrl, opt => 
                opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url));
            
            
        }
    }
}