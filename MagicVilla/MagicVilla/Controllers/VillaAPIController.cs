using AutoMapper;
using MagicVilla.Loggings;
using MagicVilla.Models;
using MagicVilla.Models.DTO;
using MagicVilla.UOW;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVilla.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogging _logger;
        protected APIResponse _response;

        public VillaAPIController(IUnitOfWork unitOfWork, IMapper mapper, ILogging logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _response = new APIResponse();
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetVillas()
        {
            _logger.Log("Getting all Villas", "");
            try
            {
                var villaList = await _unitOfWork.Villas.GetAllVillasAsync();
                _response.CompileResult(HttpStatusCode.OK, villaList);
            }
            catch (Exception ex)
            {
                _logger.Log("Getting all Villas Error", "error");
                _response.CompileError(HttpStatusCode.InternalServerError, ex);
            }

            return _response;
        }

        [HttpGet("{id:int}", Name = "GetVillaById")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetVillaById(int id)
        {
            if (id <= 0)
            {
                _logger.Log("Get Villa Error with ID: " + id, "error");
                return BadRequest();
            }

            var villa = await _unitOfWork.Villas.GetVillaByIdAsync(id);

            if (villa == null)
            {
                return NotFound();
            }

            _response.CompileResult(HttpStatusCode.OK, _mapper.Map<VillaDTO>(villa));
            return _response;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateVilla([FromBody] VillaCreateDTO villaCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("ModelState is not valid.");
            }

            if (villaCreateDTO == null)
            {
                return BadRequest("Villa data is null");
            }

            if (await _unitOfWork.Villas.GetVillaByNameAsync(villaCreateDTO.Name) != null)
            {
                return BadRequest("Villa already exists!");
            }

            Villa model = _mapper.Map<Villa>(villaCreateDTO);
            model.CreatedDate = DateTime.UtcNow;
            model.UpdatedDate = DateTime.UtcNow;

            try
            {
                _unitOfWork.CreateTransaction();
                await _unitOfWork.Villas.InsertAsync(model);
                await _unitOfWork.Save();
                _unitOfWork.Commit();

                var result = _mapper.Map<VillaUpdateDTO>(model);

                _response.CompileResult(HttpStatusCode.Created, result);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();

                _response.CompileError(HttpStatusCode.InternalServerError, ex);
            }

            return _response;
        }

        [HttpDelete("{id:int}", Name = "DeleteVillaById")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> DeleteVillaById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var villa = _unitOfWork.Villas.GetVillaByIdAsync(id);

            if (villa == null)
            {
                return NotFound();
            }

            try
            {
                await _unitOfWork.Villas.DeleteAsync(id);
                await _unitOfWork.Save();
                _unitOfWork.Commit();

                _response.CompileResult(HttpStatusCode.OK, new { });
            }
            catch (Exception ex)
            {
                //Rollback Transaction
                _unitOfWork.Rollback();

                _response.CompileError(HttpStatusCode.InternalServerError, ex);
            }

            return _response;
        }

        [HttpPut("{id:int}", Name = "UpdateVillaById")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateVillaById(int id, [FromBody] VillaUpdateDTO villaUpdateDTO)
        {
            if (villaUpdateDTO == null)
            {
                return BadRequest("Villa update data is null.");
            }

            if (id <= 0)
            {
                return BadRequest("Invalid ID.");
            }

            var existingVilla = await _unitOfWork.Villas.GetVillaByIdAsync(id);
            if (existingVilla == null)
            {
                return NotFound("Villa not found.");
            }

            villaUpdateDTO.Id = existingVilla.Id;
            _mapper.Map(villaUpdateDTO, existingVilla);
            existingVilla.UpdatedDate = DateTime.UtcNow;

            try
            {
                await _unitOfWork.Villas.UpdateAsync(existingVilla);

                //Save Changes to database
                await _unitOfWork.Save();

                //Commit the Changes to database
                _unitOfWork.Commit();

                _response.CompileResult(HttpStatusCode.OK, existingVilla);
            }
            catch (Exception ex)
            {
                //Rollback Transaction
                _unitOfWork.Rollback();

                _response.CompileError(HttpStatusCode.InternalServerError, ex);
            }

            return _response;
        }


        [HttpPatch("{id:int}", Name = "UpdatePartialVillaById")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdatePartialVillaById(int id, JsonPatchDocument<VillaUpdateDTO> patchDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (patchDTO == null || id <= 0)
            {
                return BadRequest("Invalid ID or no data.");
            }

            var existingVilla = await _unitOfWork.Villas.GetVillaByIdAsync(id);

            if (existingVilla == null)
            {
                return NotFound("Villa not found.");
            }

            var villaUpdateDTO = _mapper.Map<VillaUpdateDTO>(existingVilla);

            patchDTO.ApplyTo(villaUpdateDTO, ModelState);

            villaUpdateDTO.Id = existingVilla.Id;
            _mapper.Map(villaUpdateDTO, existingVilla);
            existingVilla.UpdatedDate = DateTime.UtcNow;
            try
            {
                await _unitOfWork.Villas.UpdateAsync(existingVilla);

                //Save Changes to database
                await _unitOfWork.Save();

                //Commit the Changes to database
                _unitOfWork.Commit();

                _response.CompileResult(HttpStatusCode.OK, existingVilla);
            }
            catch (Exception ex)
            {
                //Rollback Transaction
                _unitOfWork.Rollback();

                _response.CompileError(HttpStatusCode.InternalServerError, ex);
            }

            return _response;
        }
    }
}