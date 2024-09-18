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
    public class WorkItemTypeController : ControllerBase
    {
        private ApplicationDbContext _context { get; }
        private IMapper _mapper { get; }
        public WorkItemTypeController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Create")]

        public async Task<IActionResult> CreateWorkItemType([FromBody] WorkItemTypeCreateDto dto) 
        {
            WorkItemType newWorkItemType = _mapper.Map<WorkItemType>(dto);
            await _context.WorkItemTypes.AddAsync(newWorkItemType);
            await _context.SaveChangesAsync();
            return Ok("WorkItemType create successfully");
        }

        [HttpGet]
        [Route("Get")]

        public async Task<ActionResult<IEnumerable<WorkItemTypeGetDto>>> GetWorkItemType()
        {
            var WorkItemTypes = await _context.WorkItemTypes.Include(workItemType => workItemType.Space).ToListAsync();
            var convertedWorkItemTypes = _mapper.Map<IEnumerable<WorkItemTypeGetDto>>(WorkItemTypes);
            return Ok(convertedWorkItemTypes);
        }

        [HttpGet("WorkItemTypes/{workItemTypeId}")]
        public async Task<ActionResult<WorkItemTypeGetDto>> GetWorkItemTypeById(int workItemTypeId)
        {
            try
            {
                var workItemType = await _context.WorkItemTypes
                    .Include(wit => wit.Space)  // Uključujemo Space kako bismo dobili informacije o prostoru povezanom s work item type-om
                    .FirstOrDefaultAsync(wit => wit.Id == workItemTypeId);

                if (workItemType == null)
                {
                    return NotFound($"WorkItemType with ID {workItemTypeId} not found.");
                }

                var convertedWorkItemType = _mapper.Map<WorkItemTypeGetDto>(workItemType);
                return Ok(convertedWorkItemType);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to retrieve work item type: {ex.Message}");
            }
        }

        [HttpGet("BySpace/{spaceId}")]
        public async Task<ActionResult<IEnumerable<WorkItemTypeGetDto>>> GetSpacesByUser(long spaceId)
        {
            try
            {
                var workItemTypes = await _context.WorkItemTypes
                    .Include(workItemType => workItemType.Space)
                    .Where(workItemType => _context.WorkItemTypes.Any(us => us.Id == workItemType.Id && us.SpaceId == spaceId))
                    .ToListAsync();

                if (workItemTypes == null || workItemTypes.Count() == 0)
                {
                    return NotFound($"Spaces for user with ID {spaceId} not found.");
                }

                var convertedWorkItemTypes = _mapper.Map<IEnumerable<WorkItemTypeGetDto>>(workItemTypes);
                return Ok(convertedWorkItemTypes);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to retrieve spaces: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("UpdateName")]
        public async Task<IActionResult> UpdateTypeName([FromBody] UpdateTypeNameDto dto)
        {
            var itemType = await _context.WorkItemTypes.FindAsync(dto.Id);
            if (itemType == null)
            {
                return NotFound("State not found");
            }

            _mapper.Map(dto, itemType);
            await _context.SaveChangesAsync();
            return Ok(new { message = "ItemState finalStatuis updated successfully" });
        }

        [HttpDelete("{workItemTypeId}")]
        public async Task<IActionResult> DeleteWorkItemTypeById(long workItemTypeId)
        {
            try
            {
                var workItemTypeToDelete = await _context.WorkItemTypes.FindAsync(workItemTypeId);

                if (workItemTypeToDelete == null)
                {
                    return NotFound($"WorkItemType with ID {workItemTypeId} not found.");
                }

                _context.WorkItemTypes.Remove(workItemTypeToDelete);
                await _context.SaveChangesAsync();

                return Ok($"WorkItemType with ID {workItemTypeId} deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to delete work item type: {ex.Message}");
            }
        }


    }
}
