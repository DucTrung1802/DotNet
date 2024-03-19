using AutoMapper;
using AutomapperDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace AutomapperDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        //Create a variable to holder mapper instance
        private readonly IMapper _mapper;

        //Framework will inject the instance using Constructor
        public EmployeeController(IMapper mapper)
        {
            //Initialize the variable with the injected mapper instance
            _mapper = mapper;
        }

        private List<Employee> listEmployees = new List<Employee>()
        {
            new Employee(){ Id = 1, Name = "Anurag", Age = 28, Salary=1000, Gender = "Male", Email = "Anurag@Example.com", SocialSecurityNumber="1234@Anurag" },
            new Employee(){ Id = 2, Name = "Pranaya", Age = 30, Salary=2000, Gender = "Male", Email = "Pranaya@Example.com", SocialSecurityNumber="4567@Pranaya" },
        };

        [HttpGet]
        public ActionResult<List<EmployeeDTO>> GetEmployees()
        {
            // Use AutoMapper to map from Employee to EmployeeDTO
            List<EmployeeDTO> employees = _mapper.Map<List<EmployeeDTO>>(listEmployees);

            return Ok(employees);
        }

        [HttpPost]
        public ActionResult<EmployeeDTO> AddEmployee(EmployeeDTO employee)
        {
            if (employee != null && employee.Id == 0)
            {
                // Use AutoMapper to map from EmployeeDTO to Employee
                Employee emp = _mapper.Map<Employee>(employee);

                //Mannually Set the Properties which are not aviable in DTO
                emp.Id = listEmployees.Count + 1;
                emp.Salary = 3000;
                emp.SocialSecurityNumber = $"2356@{emp.Name}";

                //Adding Employee Object into the Database
                listEmployees.Add(emp);

                //Setting the Employee ID in EmployeeDTO
                employee.Id = emp.Id;

                //Returning the EmployeeDTO
                return Ok(employee);
            }

            //If the Incoming Data in not Valid Return Bad Requestr
            return BadRequest();
        }
    }
}