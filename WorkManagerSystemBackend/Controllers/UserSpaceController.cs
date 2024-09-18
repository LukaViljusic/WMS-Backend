using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkManagerSystemBackend.Core.Context;
using WorkManagerSystemBackend.Core.Dtos.Space;
using WorkManagerSystemBackend.Core.Dtos.UserSpace;
using WorkManagerSystemBackend.Core.Entities;

namespace WorkManagerSystemBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSpaceController : ControllerBase
    {
        private ApplicationDbContext _context { get; }
        private IMapper _mapper { get; }
        public UserSpaceController(ApplicationDbContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Create")]

        public async Task<IActionResult> CreateUserSpace([FromBody] CreateUserSpaceDto dto)
        {
            if (_context.UserSpaces.Any(u => u.SpaceId == dto.SpaceId && u.UsersId == dto.UsersId))
            {
                return Conflict("UserSpace with the same SpaceId and UserId already exists");
            }
            UserSpace newUserspace = _mapper.Map<UserSpace>(dto);
            await _context.UserSpaces.AddAsync(newUserspace);
            await _context.SaveChangesAsync();

            return Ok("UserSpace created successfully");
        }

        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<GetUserSpaceDto>>> GetUserSpaces()
        {
            var userSpaces = await _context.UserSpaces
                .Include(us => us.Users)
                .Include(us => us.Space) 
                .ToListAsync();

            var convertedUserSpaces = _mapper.Map<IEnumerable<GetUserSpaceDto>>(userSpaces);
            return Ok(convertedUserSpaces); 
        }

        [HttpGet("GetByUserId/{userId}")]
        public async Task<ActionResult<IEnumerable<GetUserSpaceDto>>> GetUserSpacesByUserId(long userId)
        {
            var userSpaces = await _context.UserSpaces
                .Include(us => us.Users)
                .Include(us => us.Space)
                .Where(us => us.UsersId == userId)
                .ToListAsync();

            if (userSpaces == null || !userSpaces.Any())
            {
                return NotFound($"UserSpaces for user with ID {userId} not found.");
            }

            var convertedUserSpaces = _mapper.Map<IEnumerable<GetUserSpaceDto>>(userSpaces);

            return Ok(convertedUserSpaces);
        }

        [HttpGet("GetBySpaceId/{spaceId}")]
        public async Task<ActionResult<IEnumerable<GetUserSpaceDto>>> GetUserSpacesBySpaceId(long spaceId)
        {
            var userSpaces = await _context.UserSpaces
                .Include(us => us.Users)
                .Include(us => us.Space)
                .Where(us => us.SpaceId == spaceId)
                .ToListAsync();

            if (userSpaces == null || !userSpaces.Any())
            {
                return NotFound($"UserSpaces for user with ID {spaceId} not found.");
            }

            var convertedUserSpaces = _mapper.Map<IEnumerable<GetUserSpaceDto>>(userSpaces);

            return Ok(convertedUserSpaces);
        }

        [HttpDelete("deleteUserSpaceBySpaceId/{spaceId}")]
        public async Task<IActionResult> DeleteUserSpacesBySpaceId(long spaceId)
        {
            try
            {
                var userSpacesToDelete = await _context.UserSpaces
                    .Where(us => us.SpaceId == spaceId)
                    .ToListAsync();

                if (userSpacesToDelete == null || userSpacesToDelete.Count == 0)
                {
                    return NotFound($"No UserSpaces found for SpaceId {spaceId}");
                }

                _context.UserSpaces.RemoveRange(userSpacesToDelete);
                await _context.SaveChangesAsync();

                return Ok($"All UserSpaces for SpaceId {spaceId} have been deleted successfully!");

            }
            catch (Exception ex)
            {

                return BadRequest($"Failed to delete UserSpace: {ex.Message}");
            }
        }

        [HttpDelete("{userSpaceId}")]
        public async Task<IActionResult> DeleteUserSpaceById(long userSpaceId)
        {
            try
            {
                var userSpaceToDelete = await _context.UserSpaces.FindAsync(userSpaceId);

                if (userSpaceToDelete == null)
                {
                    return NotFound($"UserSpace with ID {userSpaceToDelete} not found.");
                }

                _context.UserSpaces.Remove(userSpaceToDelete);
                await _context.SaveChangesAsync();

                return Ok($"Userspace with ID {userSpaceId} deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to delete userspace: {ex.Message}");
            }
        }

    }
}
