using MagicVilla.Models;
using MagicVilla.Models.DTO;
using MagicVilla.UOW;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVilla.Controllers
{
    [Route("api/UsersAuth")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        protected APIResponse _response;

        public UserController(IUnitOfWork unitOfWork, APIResponse response)
        {
            _unitOfWork = unitOfWork;
            _response = new APIResponse();
        }

        [HttpPost("login")]
        public async Task<ActionResult<APIResponse>> Login([FromBody] LoginRequestDTO model)
        {
            var loginResponse = await _unitOfWork.Users.Login(model);

            if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                _response.CompileError(HttpStatusCode.BadRequest, new Exception("Username or password is incorrect"));
                return _response;
            }

            _response.CompileResult(HttpStatusCode.OK, loginResponse);
            return _response;
        }

        [HttpPost("register")]
        public async Task<ActionResult<APIResponse>> Register([FromBody] RegistrationRequestDTO model)
        {
            bool ifUserNameUnique = _unitOfWork.Users.IsUniqueUser(model.UserName);

            if (!ifUserNameUnique)
            {
                _response.CompileError(HttpStatusCode.BadRequest, new Exception("Username already exists!"));
                return _response;
            }

            var user = await _unitOfWork.Users.Register(model);

            if (user == null)
            {
                _response.CompileError(HttpStatusCode.BadRequest, new Exception("Error while registering"));
                return _response;
            }

            _response.CompileResult(HttpStatusCode.OK, new { });
            return _response;
        }
    }
}
