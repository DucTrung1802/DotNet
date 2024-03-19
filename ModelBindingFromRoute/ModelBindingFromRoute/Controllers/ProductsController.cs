using Microsoft.AspNetCore.Mvc;
using ModelBinding.Models;

namespace ModelBinding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private static List<Product> Products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Categogy = "Electronics", Price = 1000, Quantity = 10 },
            new Product { Id = 2, Name = "Desktop", Categogy = "Electronics", Price = 2000, Quantity = 20 },
            new Product { Id = 3, Name = "Mobile", Categogy = "Electronics", Price = 3000, Quantity = 30 },
            new Product { Id = 4, Name = "Casual Shirts", Categogy = "Apparel", Price = 500, Quantity = 10 },
            new Product { Id = 5, Name = "Formal Shirts", Categogy = "Apparel", Price = 600, Quantity = 30 },
            new Product { Id = 6, Name = "Jackets & Coats", Categogy = "Apparel", Price = 700, Quantity = 20 },
        };

        [HttpGet("{id}")]
        public IActionResult GetProductById([FromRoute] int id)
        {
            // Logic to retrieve the user by ID
            var product = Products.FirstOrDefault(prd => prd.Id == id);
            if (product != null)
            {
                return Ok(product);
            }
            return NotFound($"No Product Found with Product Id: {id}");
        }
    }
}