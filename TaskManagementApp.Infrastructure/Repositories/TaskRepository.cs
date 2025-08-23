using Microsoft.EntityFrameworkCore;
using TaskManagementApp.Domain.Interfaces;
using TaskManagementApp.Domain.Models;

namespace TaskManagementApp.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly DatabaseContext _databaseContext;

    public TaskRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<TaskItem?> GetByIdAsync(Guid id) =>
        await _databaseContext.Tasks.FindAsync(id);

    public async Task<List<TaskItem>> GetAllAsync() =>
        await _databaseContext.Tasks.ToListAsync();

    public async Task AddAsync(TaskItem task)
    {
        await _databaseContext.Tasks.AddAsync(task);
    }
    
    public Task Update(TaskItem  task)
    {
        _databaseContext.Tasks.Update(task);
        return Task.CompletedTask;
    }
    
    public async Task DeleteAsync(Guid id)
    {
        var task = await GetByIdAsync(id);
        if (task != null)
        {
            _databaseContext.Tasks.Remove(task);
        }
    }
    
    public async Task SaveChangesAsync() => await _databaseContext.SaveChangesAsync();
}