using AutoMapper;
using AutomapperDemo.Models;
using Microsoft.AspNetCore.Mvc;
using MyFirstWebAPIProject.Models;

namespace AutomapperDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;

        public UserController(IMapper mapper)
        {
            _mapper = mapper;
        }

        private List<User> listUsers = new List<User>()
        {
           new User { UserId = 1, Name="Pranaya", Email="Pranaya@Example.come",
               Address = new Address()
               {
                  Country = "India",
                  State = "Odisha",
                  City = "BBSR",
                  AddressId = 1001,
                  UserId = 1,
               }
           },
           new User { UserId = 2, Name="Priyanka", Email="Priyanka@Example.come",
               Address = new Address()
               {
                  Country = "India",
                  State = "Maharashtra",
                  City = "Mumbai",
                  AddressId = 1002,
                  UserId = 2,
               }
           }
        };

        [HttpGet]
        public ActionResult<List<User>> GetAllUsers()
        {
            List<UserDTO> users = _mapper.Map<List<UserDTO>>(listUsers);
            return Ok(users);
        }
    }
}