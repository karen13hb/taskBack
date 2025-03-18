using Microsoft.AspNetCore.Mvc;
using Tasks.Application.DTOs;
using Tasks.Application.Interfaces;
using Tasks.Application.Services;
using Tasks.Domain.Entities;

namespace Tasks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(TaskService taskService)
        {
            _taskService = taskService;
        }

        // GET: api/Task
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTasks()
        {
            var tasks = await _taskService.GetTasksAsync();
            return Ok(tasks);
        }

        // GET: api/Task/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetTask(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
                return NotFound();

            return Ok(task);
        }

        // POST: api/Task
        [HttpPost]
        public async Task<ActionResult> CreateTask(CreateTaskDto createTaskDtotask)
        {
            var task = new TaskItem
            {
                Title = createTaskDtotask.Title,
                Description = createTaskDtotask.Description,
                AssignedTo = createTaskDtotask.AssignedTo,
                DueDate = createTaskDtotask.DueDate,
                Completed = createTaskDtotask.Completed,

            };

            await _taskService.CreateTaskAsync(task);
            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }

        // PUT: api/Task/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTask(int id, UpdateTaskDto updateTaskDto)
        {

            try
            {
                var taskUpdate = new TaskItem
                {
                    Id = id,
                    Title = updateTaskDto.Title,
                    Description = updateTaskDto.Description,
                    AssignedTo = updateTaskDto.AssignedTo,
                    DueDate = updateTaskDto.DueDate,
                    Completed = updateTaskDto.Completed,
                };
                await _taskService.UpdateTaskAsync(taskUpdate);

                return Ok(taskUpdate);

            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ha ocurrido un error: {ex.Message}");
            }  
        }

        // DELETE: api/Task/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTask(int id)
        {
            try
            {
                await _taskService.DeleteTaskAsync(id);
                return Ok(new { message = "Tarea eliminada correctamente" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ha ocurrido un error: {ex.Message}");
            }
        }
    }
}
