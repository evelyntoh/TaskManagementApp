using Microsoft.AspNetCore.Mvc;
using TaskManagementApp.Application.Services;
using TaskManagementApp.Domain.Models;

namespace TaskManagementApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly TaskService _taskService;

    public TasksController(TaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public async Task<IEnumerable<TaskItem>> GetTasks()
    {
        return await _taskService.GetTasksAsync();
    }
    
    [HttpGet("complete")]
    public async Task<ActionResult<List<TaskItem>>> GetCompletedTasks()
    {
        var tasks = await _taskService.GetCompleteAsync();
        return Ok(tasks);
    }

    [HttpGet("incomplete")]
    public async Task<ActionResult<List<TaskItem>>> GetIncompleteTasks()
    {
        var tasks = await _taskService.GetIncompleteAsync();
        return Ok(tasks);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TaskItem>> GetTaskById(Guid id)
    {
        var task = await _taskService.GetTaskByIdAsync(id);
        if (task == null)
            return NotFound();
        return Ok(task);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddTask(TaskItem task)
    {
        await _taskService.AddTaskAsync(task);
        return NoContent();    
    }
    
    [HttpPut("{id:guid}/complete")]
    public async Task<ActionResult> CompleteTask(Guid id)
    {
        await _taskService.CompleteTaskAsync(id);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTask(Guid id)
    {
        await _taskService.DeleteTaskAsync(id);
        return NoContent();
    }
}