using TaskManagementApp.Domain.Interfaces;
using TaskManagementApp.Domain.Models;

namespace TaskManagementApp.Application.Services;

public class TaskService
{
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<List<TaskItem>> GetTasksAsync() =>
        await _taskRepository.GetAllAsync();

    public async Task<TaskItem?> GetTaskByIdAsync(Guid id) =>
        await _taskRepository.GetByIdAsync(id);

    public async Task<List<TaskItem>> GetCompleteAsync()
    {
        var tasks = await GetTasksAsync();
        return tasks.Where(t => t.IsCompleted).ToList();
    }

    public async Task<List<TaskItem>> GetIncompleteAsync()
    {
        var tasks = await GetTasksAsync();
        return tasks.Where(t => !t.IsCompleted).ToList();
    }

    public async Task AddTaskAsync(TaskItem task)
    {
        await _taskRepository.AddAsync(task);
        await _taskRepository.SaveChangesAsync();
    }
    
    public async Task CompleteTaskAsync(Guid id)
    {
        var task = await GetTaskByIdAsync(id);
        if (task != null)
        {
            task.IsCompleted = true;
            await _taskRepository.Update(task);
            await _taskRepository.SaveChangesAsync();
        }
    }
        
    public async Task DeleteTaskAsync(Guid id)
    {
        await _taskRepository.DeleteAsync(id);
        await _taskRepository.SaveChangesAsync();
    }
}
