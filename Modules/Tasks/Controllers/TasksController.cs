using TmModule.Modules.Tasks.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TmModule.Modules.Tasks.Models;
using System.Xml;
using TmModule.Application.CoreController;

namespace TmModule.Modules.Tasks.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class TasksController : ControllerBase
    {
        private readonly TaskService _service;
        public TasksController(TaskService service)
        {
            _service = service;
        }

        // GET: api/Tasks
        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await _service.GetAllTasksAsync();
            return Ok(tasks);
        }

        // GET: api/Tasks/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _service.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        // POST: api/Tasks
        [HttpPost]
        public async Task<IActionResult> AddTask([FromBody] TaskTable task)
        {
            if (task == null || string.IsNullOrEmpty(task.Description))
            {
                return BadRequest("Invalid task data.");
            }

            if (string.IsNullOrWhiteSpace(task.Description))
            {
                return BadRequest("Task description cannot be empty or whitespace.");
            }

            await _service.AddTaskAsync(task);
            return CreatedAtAction(nameof(GetTaskById), new { id = task.TaskId }, task);
        }
    }
}
