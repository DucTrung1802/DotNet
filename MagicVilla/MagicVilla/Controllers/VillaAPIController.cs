using AutoMapper;
using MagicVilla.Loggings;
using MagicVilla.Models;
using MagicVilla.Models.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        private readonly ILogging _logger;

        private readonly ApplicationDbContext _context;

        private readonly IMapper _mapper;

        public VillaAPIController(ILogging logger, ApplicationDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaCreateDTO>>> GetVillas()
        {
            _logger.Log("Getting all Villas", "");
            var villaList = await _context.Villas.ToListAsync();
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

            var villa = await _context.Villas.FirstOrDefaultAsync(v => v.Id == id);
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

            if (await _context.Villas.FirstOrDefaultAsync(v => v.Name.ToLower() == villaCreateDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Villa already exists!");
                return BadRequest();
            }

            Villa model = _mapper.Map<Villa>(villaCreateDTO);
            model.CreatedDate = DateTime.UtcNow;
            model.UpdatedDate = DateTime.UtcNow;

            var result = _mapper.Map<VillaUpdateDTO>(model);

            await _context.Villas.AddAsync(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVillaById), new { id = model.Id }, result);
        }

        [HttpDelete("{id:int}", Name = "DeleteVillaById")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteVillaById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var villa = _context.Villas.FirstOrDefault(v => v.Id == id);

            if (villa == null)
            {
                return NotFound();
            }

            _context.Villas.Remove(villa);
            _context.SaveChanges();

            return NoContent();
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

            var existingVilla = await _context.Villas.FindAsync(id);
            if (existingVilla == null)
            {
                return NotFound("Villa not found.");
            }

            villaUpdateDTO.Id = existingVilla.Id;
            _mapper.Map(villaUpdateDTO, existingVilla);
            existingVilla.UpdatedDate = DateTime.UtcNow;

            _context.Villas.Update(existingVilla);
            await _context.SaveChangesAsync();

            return Ok(villaUpdateDTO);
        }


        [HttpPatch("{id:int}", Name = "UpdatePartialVillaById")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialVillaById(int id, JsonPatchDocument<VillaUpdateDTO> patchDTO)
        {
            if (patchDTO == null || id <= 0)
            {
                return BadRequest("Invalid ID or no data.");
            }

            var existingVilla = await _context.Villas.FindAsync(id);

            if (existingVilla == null)
            {
                return NotFound("Villa not found.");
            }

            var villaUpdateDTO = _mapper.Map<VillaUpdateDTO>(existingVilla);

            patchDTO.ApplyTo(villaUpdateDTO, ModelState);

            villaUpdateDTO.Id = existingVilla.Id;
            _mapper.Map(villaUpdateDTO, existingVilla);
            existingVilla.UpdatedDate = DateTime.UtcNow;

            _context.Villas.Update(existingVilla);
            await _context.SaveChangesAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(villaUpdateDTO);
        }
    }
}
