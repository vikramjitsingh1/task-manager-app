using Microsoft.AspNetCore.Mvc;
using TmModule.Modules.Status.Services;
using TmModule.Modules.Status.Models;
using Microsoft.IdentityModel.Tokens;

namespace TmModule.Modules.Status.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class StatusController : ControllerBase
    {
        private readonly StatusUpdateService _service;
        public StatusController(StatusUpdateService service)
        {
            _service = service;
        }

        // GET: api/Status/{userId}/{taskId}
        [HttpGet("{userId}/{taskId}")]
        public async Task<IActionResult> GetResult(int userId, int taskId)
        {
            var status = await _service.GetStatusAsync(userId, taskId);
            if (status == null)
            {
                return NotFound();
            }
            return Ok(status);
        }

        // POST: api/Status
        [HttpPost]
        public async Task<IActionResult> AssignTask([FromBody] StatusTable status)
        {
            if (status == null || status.UserId <= 0 || status.TaskId <= 0)
            {
                return BadRequest("Invalid status data.");
            }
            try
            {
                await _service.AssignTask(status.UserId, status.TaskId);
                return Ok("Task assigned successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PATCH: api/Status
        [HttpPatch]
        public async Task<IActionResult> UpdateStatus([FromBody] StatusTable status)
        {
            if (status == null || status.UserId <= 0 || status.TaskId <= 0)
            {
                return BadRequest("Invalid status data.");
            }
            try
            {
                await _service.UpdateStatus(status.UserId, status.TaskId, status.CurrentStatus);
                return Ok("Status updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}