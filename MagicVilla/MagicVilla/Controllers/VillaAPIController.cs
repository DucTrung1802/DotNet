using AutoMapper;
using MagicVilla.Loggings;
using MagicVilla.Models;
using MagicVilla.Models.DTO;
using MagicVilla.UOW;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        private readonly ILogging _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public VillaAPIController(IUnitOfWork unitOfWork, IMapper mapper, ILogging logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaCreateDTO>>> GetVillas()
        {
            _logger.Log("Getting all Villas", "");
            var villaList = await _unitOfWork.Villas.GetAllVillasAsync();
            return Ok(_mapper.Map<List<VillaDTO>>(villaList));
        }

        [HttpGet("{id:int}", Name = "GetVillaById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaCreateDTO>> GetVillaById(int id)
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
            return Ok(_mapper.Map<VillaDTO>(villa));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaUpdateDTO>> CreateVilla([FromBody] VillaCreateDTO villaCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (villaCreateDTO == null)
            {
                ModelState.AddModelError("BadRequest", "Villa data is null");
                return BadRequest(villaCreateDTO);
            }

            if (await _unitOfWork.Villas.GetVillaByNameAsync(villaCreateDTO.Name) != null)
            {
                ModelState.AddModelError("CustomError", "Villa already exists!");
                return BadRequest();
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

                return CreatedAtAction(nameof(GetVillaById), new { id = model.Id }, result);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
            }

            return APIResponse;
        }

        [HttpDelete("{id:int}", Name = "DeleteVillaById")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async IActionResult DeleteVillaById(int id)
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

                //Save Changes to database
                await _unitOfWork.Save();

                //Commit the Changes to database
                _unitOfWork.Commit();

                return Ok();
            }
            catch (Exception)
            {
                //Rollback Transaction
                _unitOfWork.Rollback();

                return APISResponse
            }
        }

        [HttpPut("{id:int}", Name = "UpdateVillaById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<VillaCreateDTO>> UpdateVillaById(int id, [FromBody] VillaUpdateDTO villaUpdateDTO)
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

                return Ok(villaUpdateDTO);
            }
            catch (Exception)
            {
                //Rollback Transaction
                _unitOfWork.Rollback();

                return APIResponse
            }
        }


        [HttpPatch("{id:int}", Name = "UpdatePartialVillaById")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialVillaById(int id, JsonPatchDocument<VillaUpdateDTO> patchDTO)
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
            existingVilla.UpdatedDate = DateTime.UtcNow

            try
            {
                await _unitOfWork.Villas.UpdateAsync(existingVilla);

                //Save Changes to database
                await _unitOfWork.Save();

                //Commit the Changes to database
                _unitOfWork.Commit();

                return Ok(villaUpdateDTO);
            }
            catch (Exception)
            {
                //Rollback Transaction
                _unitOfWork.Rollback();

                return APIResponse
            }
        }
    }
}
