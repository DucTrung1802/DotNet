using Microsoft.AspNetCore.Mvc;
using ModelBinding.Models;
namespace ModelBinding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetResource([ModelBinder] FilterModel filter)
        {
            // Use the bound complex object
            return Ok(filter);
        }
    }
}