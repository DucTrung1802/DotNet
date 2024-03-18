using AutoMapper;
using MyFirstWebAPIProject.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace AutomapperDemo.Models
{
    public class MyMappingProfile : Profile
    {
        public MyMappingProfile()
        {
            //Configure the Mappings
            CreateMap<User, UserDTO>()
               //Provide Mapping Information for City Property
               .ForMember(dest => dest.City, act => act.MapFrom(src => src.Address.City))
               //Provide Mapping Information for State Property
               .ForMember(dest => dest.State, act => act.MapFrom(src => src.Address.State))
               //Provide Mapping Information for Country Property
               .ForMember(dest => dest.Country, act => act.MapFrom(src => src.Address.Country));
        }
    }
}