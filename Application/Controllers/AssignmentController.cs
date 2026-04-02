using Microsoft.AspNetCore.Mvc;
using TmModule.Application.UseCases;
using TmModule.Infrastructure.DTOs;

namespace TmModule.Application.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AssignmentController(
       AssignTaskToUser assignmentService,
        DetailedView detailedViewService,
        CreateUserTask createUserTaskService) : ControllerBase
    {
        private readonly AssignTaskToUser _assignmentService = assignmentService;
        private readonly DetailedView _detailedViewService = detailedViewService;
        private readonly CreateUserTask _createUserTaskService = createUserTaskService;

        // POST: api/Assignment/create
        [HttpPost("create")]
        public async Task<IActionResult> CreateUserTask([FromBody] SimplifiedRequest simplifiedRequest)
        {
            try
            {
                await _createUserTaskService.CreateUserTaskAsync(simplifiedRequest);
                return Ok("User task created successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Assignment/assign
        [HttpPost("assign")]
        public async Task<IActionResult> Assign([FromBody] AssignTaskToUserRequest assignmentRequest)
        {
            try
            {
                if (assignmentRequest == null)
                    return BadRequest("Assignment request cannot be null.");

                await _assignmentService.AssignTasksToUsersAsync(assignmentRequest);
                return Ok("Tasks assigned successfully.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest($"Invalid Input:{ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Something went wrong while assigning tasks: {ex.Message}");
            }
        }

        // GET: api/Assignment/details
        [HttpGet("details")]
        public async Task<IActionResult> GetAllDetails()
        {
            try
            {
                var details = await _detailedViewService.GetAllDetails();
                return Ok(details);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

