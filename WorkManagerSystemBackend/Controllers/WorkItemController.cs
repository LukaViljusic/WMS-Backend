using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkManagerSystemBackend.Core.Context;
using WorkManagerSystemBackend.Core.Dtos.Space;
using WorkManagerSystemBackend.Core.Dtos.WorkItem;
using WorkManagerSystemBackend.Core.Dtos.WorkItemState;
using WorkManagerSystemBackend.Core.Dtos.WorkItemType;
using WorkManagerSystemBackend.Core.Entities;

namespace WorkManagerSystemBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkItemController : ControllerBase
    {
        private ApplicationDbContext _context { get; }
        private IMapper _mapper { get; }
        public WorkItemController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Create")]

        public async Task<IActionResult> CreateWorkItem([FromBody] WorkItemCreateDto dto)
        {
            WorkItem newWorkItem = _mapper.Map<WorkItem>(dto);
            await _context.WorkItems.AddAsync(newWorkItem);
            await _context.SaveChangesAsync();

            return Ok("WorkItemState Create Successfully");
        }

        [HttpGet]
        [Route("Get")]

        public async Task<ActionResult<IEnumerable<WorkItemGetDto>>> GetWorkItem()
        {
            var WorkItems = await _context.WorkItems
                .Include(workItem => workItem.Users)
                .Include(workItem => workItem.Space)
                .Include(workItem => workItem.WorkItemType)
                .Include(workItem => workItem.WorkItemState)
                .ToListAsync();
            var convertedWorkItems = _mapper.Map<IEnumerable<WorkItemGetDto>>(WorkItems);
            return Ok(convertedWorkItems);
        }

        [HttpGet("WorkItems/{workItemId}")]
        public async Task<ActionResult<WorkItemGetDto>> GetWorkItemById(int workItemId)
        {
            try
            {
                var workItem = await _context.WorkItems
                    .Include(workItem => workItem.Users)
                    .Include(workItem => workItem.Space)
                    .Include(workItem => workItem.WorkItemType)
                    .Include(workItem => workItem.WorkItemState)
                    .FirstOrDefaultAsync(wi => wi.Id == workItemId);

                if (workItem == null)
                {
                    return NotFound($"WorkItem with ID {workItemId} not found.");
                }

                var convertedWorkItem = _mapper.Map<WorkItemGetDto>(workItem);
                return Ok(convertedWorkItem);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to retrieve work item: {ex.Message}");
            }
        }

        [HttpGet("ByUser/{userId}")]
        public async Task<ActionResult<IEnumerable<WorkItemGetDto>>> GetSpacesByUser(long userId)
        {
            try
            {
                var workItems = await _context.WorkItems
                    .Include(workItem => workItem.Users)
                    .Include(workItem => workItem.Space)
                    .Include(workItem => workItem.WorkItemType)
                    .Include(workItem => workItem.WorkItemState)
                    .Where(workItem => _context.WorkItems.Any(us => us.Id == workItem.Id && us.UsersId == userId))
                    .ToListAsync();

                if (workItems == null || workItems.Count() == 0)
                {
                    return NotFound($"WorkItems for user with ID {userId} not found.");
                }

                var convertedworkItems = _mapper.Map<IEnumerable<WorkItemGetDto>>(workItems);
                return Ok(convertedworkItems);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to retrieve spaces: {ex.Message}");
            }
        }

        [HttpGet("BySpace/{spaceId}")]
        public async Task<ActionResult<IEnumerable<WorkItemGetDto>>> GetWorkItemsBySpaceId(long spaceId)
        {
            try
            {
                var workItems = await _context.WorkItems
                    .Include(workItem => workItem.Users)
                    .Include(workItem => workItem.Space)
                    .Include(workItem => workItem.WorkItemType)
                    .Include(workItem => workItem.WorkItemState)
                    .Where(workItem => _context.WorkItems.Any(us => us.Id == workItem.Id && us.SpaceId == spaceId))
                    .ToListAsync();

                if (workItems == null || workItems.Count() == 0)
                {
                    return NotFound($"WorkItems for user with ID {spaceId} not found.");
                }

                var convertedworkItems = _mapper.Map<IEnumerable<WorkItemGetDto>>(workItems);
                return Ok(convertedworkItems);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to retrieve spaces: {ex.Message}");
            }
        }

        [HttpDelete("{workItemId}")]
        public async Task<IActionResult> DeleteWorkItemById(long workItemId)
        {
            try
            {
                var workItemToDelete = await _context.WorkItems.FindAsync(workItemId);

                if (workItemToDelete == null)
                {
                    return NotFound($"WorkItem with ID {workItemId} not found.");
                }

                _context.WorkItems.Remove(workItemToDelete);
                await _context.SaveChangesAsync();

                return Ok($"WorkItem with ID {workItemId} deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to delete work item: {ex.Message}");
            }
        }


    }
}
