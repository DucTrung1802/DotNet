using AutoMapper;

namespace AutomapperDemo.Models
{
    public class MyMappingProfile : Profile
    {
        public MyMappingProfile()
        {
            //Configure the Mappings

            //Mapping Employee to EmployeeDTO
            //Source: Employee and Destination: EmployeeDTO
            CreateMap<Employee, EmployeeDTO>();

            //Mapping EmployeeDTO to Employee
            //Source: EmployeeDTO and Destination: Employee
            CreateMap<EmployeeDTO, Employee>();

            CreateMap<Category, CategoryDTO>();
        }
    }
}