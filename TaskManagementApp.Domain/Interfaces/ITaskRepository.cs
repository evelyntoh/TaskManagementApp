using TaskManagementApp.Domain.Models;

namespace TaskManagementApp.Domain.Interfaces;

public interface ITaskRepository
{
    Task<TaskItem?> GetByIdAsync(Guid id);
    Task<List<TaskItem>> GetAllAsync();
    Task AddAsync(TaskItem task);
    Task Update(TaskItem  task);
    Task DeleteAsync(Guid id);
    Task SaveChangesAsync();
}