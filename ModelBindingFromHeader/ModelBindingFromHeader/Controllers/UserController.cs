using Microsoft.AspNetCore.Mvc;
using ModelBinding.Models;
namespace ModelBinding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetResource([ModelBinder] UserPreferences userPreferences)
        {
            // Use the userPreferences object as needed
            return Ok(new { Message = "User Preferences Received", Preferences = userPreferences });
        }
    }
}