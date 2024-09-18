using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkManagerSystemBackend.Core.Context;
using WorkManagerSystemBackend.Core.Dtos.Space;
using WorkManagerSystemBackend.Core.Dtos.User;
using WorkManagerSystemBackend.Core.Entities;

namespace WorkManagerSystemBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpaceController : ControllerBase
    {
        private ApplicationDbContext _context { get; }
        private IMapper _mapper { get; }
        public SpaceController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<int>> CreateSpace([FromBody] SpaceCreateDto dto)
        {
            Space newSpace = _mapper.Map<Space>(dto);
            await _context.Spaces.AddAsync(newSpace);
            await _context.SaveChangesAsync();

            return Ok(newSpace.Id); 
        }

        [HttpGet]
        [Route("Get")]

        public async Task<ActionResult<IEnumerable<SpaceGetDto>>> GetSpaces()
        {
            var spaces = await _context.Spaces.Include(space => space.Users).ToListAsync();
            var convertedSpaces = _mapper.Map<IEnumerable<SpaceGetDto>>(spaces);
            return Ok(convertedSpaces);
        }

        [HttpGet("{spaceId}")]
        public async Task<ActionResult<SpaceGetDto>> GetSpaceById(int spaceId)
        {
            try
            {
                var space = await _context.Spaces
                    .Include(s => s.Users)
                    .FirstOrDefaultAsync(s => s.Id == spaceId);

                if (space == null)
                {
                    return NotFound($"Space with ID {spaceId} not found.");
                }

                var convertedSpace = _mapper.Map<SpaceGetDto>(space);
                return Ok(convertedSpace);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to retrieve space: {ex.Message}");
            }
        }

        [HttpGet("ByUser/{userId}")]
        public async Task<ActionResult<IEnumerable<SpaceGetDto>>> GetSpacesByUser(long userId)
        {
            try
            {
                var spaces = await _context.Spaces
                    .Include(space => space.Users)
                    .Where(space => _context.Spaces.Any(us => us.Id == space.Id && us.UsersId == userId))
                    .ToListAsync();

                if (spaces == null || spaces.Count() == 0)
                {
                    return NotFound($"Spaces for user with ID {userId} not found.");
                }

                var convertedSpaces = _mapper.Map<IEnumerable<SpaceGetDto>>(spaces);
                return Ok(convertedSpaces);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to retrieve spaces: {ex.Message}");
            }
        }



        [HttpDelete("{spaceId}")]
        public async Task<IActionResult> DeleteSpaceById(long spaceId)
        {
            try
            {
                var spaceToDelete = await _context.Spaces.FindAsync(spaceId);

                if (spaceToDelete == null)
                {
                    return NotFound($"Space with ID {spaceId} not found.");
                }

                _context.Spaces.Remove(spaceToDelete);
                await _context.SaveChangesAsync();

                return Ok($"Space with ID {spaceId} deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to delete space: {ex.Message}");
            }
        }

    }
}
