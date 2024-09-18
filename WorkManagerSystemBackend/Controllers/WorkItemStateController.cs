using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkManagerSystemBackend.Core.Context;
using WorkManagerSystemBackend.Core.Dtos.Space;
using WorkManagerSystemBackend.Core.Dtos.WorkItemState;
using WorkManagerSystemBackend.Core.Dtos.WorkItemType;
using WorkManagerSystemBackend.Core.Entities;

namespace WorkManagerSystemBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkItemStateController : ControllerBase
    {
        private ApplicationDbContext _context { get; }
        private IMapper _mapper { get; }
        public WorkItemStateController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Create")]

        public async Task<IActionResult> CreateWorkItemState([FromBody] WorkItemStateCreateDto dto)
        {
            WorkItemState newWorkItemState = _mapper.Map<WorkItemState>(dto);
            await _context.WorkItemStates.AddAsync(newWorkItemState);
            await _context.SaveChangesAsync();

            return Ok("WorkItemState Create Successfully");
        }

        [HttpGet]
        [Route("Get")]

        public async Task<ActionResult<IEnumerable<WorkItemStateGetDto>>> GetWorkItemState()
        {
            var WorkItemStates = await _context.WorkItemStates.Include(workItemState => workItemState.Space).ToListAsync();
            var convertedWorkItemStates = _mapper.Map<IEnumerable<WorkItemStateGetDto>>(WorkItemStates);
            return Ok(convertedWorkItemStates);
        }

        [HttpGet("WorkItemStates/{workItemStateId}")]
        public async Task<ActionResult<WorkItemStateGetDto>> GetWorkItemStateById(int workItemStateId)
        {
            try
            {
                var workItemState = await _context.WorkItemStates
                    .Include(wis => wis.Space)  // Uključujemo Space kako bismo dobili informacije o work item type-u
                    .FirstOrDefaultAsync(wis => wis.Id == workItemStateId);

                if (workItemState == null)
                {
                    return NotFound($"WorkItemState with ID {workItemStateId} not found.");
                }

                var convertedWorkItemState = _mapper.Map<WorkItemStateGetDto>(workItemState);
                return Ok(convertedWorkItemState);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to retrieve work item state: {ex.Message}");
            }
        }

        /*[HttpGet("ByWorkItemType/{workItemTypeId}")]
        public async Task<ActionResult<IEnumerable<WorkItemStateGetDto>>> GetSpacesByUser(long workItemTypeId)
        {
            try
            {
                var workItemState = await _context.WorkItemStates
                    .Include(workItemState => workItemState.WorkItemType)
                    .Where(workItemState => _context.WorkItemStates.Any(us => us.Id == workItemState.Id && us.WorkItemTypeId == workItemTypeId))
                    .ToListAsync();

                if (workItemState == null || workItemState.Count() == 0)
                {
                    return NotFound($"Spaces for user with ID {workItemTypeId} not found.");
                }

                var convertedWorkItemStates = _mapper.Map<IEnumerable<WorkItemStateGetDto>>(workItemState);
                return Ok(convertedWorkItemStates);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to retrieve spaces: {ex.Message}");
            }
        }*/

        [HttpGet("BySpaceId/{spaceId}")]
        public async Task<ActionResult<IEnumerable<WorkItemStateGetDto>>> GetWorkItemStatesBySpaceId(long spaceId)
        {
            try
            {
                var workItemStates = await _context.WorkItemStates
                    .Include(workItemState => workItemState.Space)
                    .Where(workItemState => _context.WorkItemStates.Any(us => us.Id == workItemState.Id && us.SpaceId == spaceId))
                    .ToListAsync();

                if (workItemStates == null || !workItemStates.Any())
                {
                    return NotFound($"WorkItemStates for Space with ID {spaceId} not found.");
                }

                var convertedWorkItemStates = _mapper.Map<IEnumerable<WorkItemStateGetDto>>(workItemStates);
                return Ok(convertedWorkItemStates);
            }
            catch (Exception ex)
            {
                // Handle exception appropriately, log, and return an error response.
                return BadRequest($"Failed to retrieve spaces: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("UpdateFinalStatus")]
        public async Task<IActionResult> UpdateFinalStatus([FromBody] UpdateFinalStateDto dto)
        {
            var itemState = await _context.WorkItemStates.FindAsync(dto.Id);
            if(itemState == null)
            {
                return NotFound("State not found");
            }

            _mapper.Map(dto, itemState);
            await _context.SaveChangesAsync();
            return Ok(new { message = "ItemState finalStatus updated successfully"});
        }

        [HttpPut]
        [Route("UpdateName")]
        public async Task<IActionResult> UpdateStateName([FromBody] UpdateStateNameDto dto)
        {
            var itemState = await _context.WorkItemStates.FindAsync(dto.Id);
            if (itemState == null)
            {
                return NotFound("State not found");
            }

            _mapper.Map(dto, itemState);
            await _context.SaveChangesAsync();
            return Ok(new { message = "ItemState finalStatuis updated successfully" });
        }


        [HttpDelete("{workItemStateId}")]
        public async Task<IActionResult> DeleteWorkItemStateById(long workItemStateId)
        {
            try
            {
                var workItemStateToDelete = await _context.WorkItemStates.FindAsync(workItemStateId);

                if (workItemStateToDelete == null)
                {
                    return NotFound($"WorkItemState with ID {workItemStateId} not found.");
                }

                _context.WorkItemStates.Remove(workItemStateToDelete);
                await _context.SaveChangesAsync();

                return Ok($"WorkItemState with ID {workItemStateId} deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to delete work item state: {ex.Message}");
            }
        }

    }
}
