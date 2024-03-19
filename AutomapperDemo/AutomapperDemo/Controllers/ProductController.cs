using AutoMapper;
using AutomapperDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace AutomapperDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        //Create a variable to holder mapper instance
        private readonly IMapper _mapper;

        //Framework will inject the instance using Constructor
        public ProductController(IMapper mapper)
        {
            //Initialize the variable with the injected mapper instance
            _mapper = mapper;
        }

        private List<Category> listCategories = new List<Category>()
        {
           new Category { Id = 1, Name="Electronics",
               Products = new List<Product>
               {
                   new Product { Id = 1001, Name="Laptop", Description="Gaming Laptop", Price = 1000, CategoryId = 1},
                   new Product { Id = 1002, Name="Desktop", Description="Programming Desktop", Price = 2000, CategoryId = 1}
               }
           },
           new Category { Id = 2, Name="Appearl",
               Products = new List<Product>
               {
                   new Product { Id = 1003, Name="T-Shrt", Description="T-Shrt with V Neck", Price = 700, CategoryId = 2},
                   new Product { Id = 1004, Name="Jacket", Description="Winter Jacket", Price = 800, CategoryId = 2}
               }
           }
        };

        [HttpGet("Categories")]
        public ActionResult<List<CategoryDTO>> GetAllCategory()
        {
            List<CategoryDTO> categories = _mapper.Map<List<CategoryDTO>>(listCategories);
            return Ok(categories);
        }
    }
}