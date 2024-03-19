using Microsoft.AspNetCore.Mvc;
using ModelBinding.Models;

namespace ModelBinding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateProduct([FromBody] List<Product> products)
        {
            // Handle multiple products
            // For demonstration, let's return the products back
            return Ok(products);
        }
    }
}

