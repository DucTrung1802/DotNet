using Microsoft.AspNetCore.Mvc;
using ModelBinding.Models;
namespace ModelBinding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromForm] UserModel user)
        {
            if (user.ProfilePicture != null)
            {
                var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
                if (!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath);
                }

                var filePath = Path.Combine(uploadsFolderPath, user.ProfilePicture.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await user.ProfilePicture.CopyToAsync(stream);
                }

                // Handle the user data, e.g., save it to a database
                var response = new
                {
                    Success = true,
                    Message = $"User {user.Name} created successfully!",
                    ProfilePictureName = user?.ProfilePicture?.FileName,
                    Code = StatusCodes.Status200OK
                };
                return Ok(response);
            }
            return BadRequest(ModelState);
        }
    }
}