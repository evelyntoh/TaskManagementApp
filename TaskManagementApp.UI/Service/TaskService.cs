using System.Net.Http.Json;
using TaskManagementApp.UI.Models;

namespace TaskManagementApp.UI.Service;

public class TaskService
{
    private readonly HttpClient _http;

    public TaskService(HttpClient http) => _http = http;

    public async Task<List<TaskItemModel>> GetTasksAsync() =>
        await _http.GetFromJsonAsync<List<TaskItemModel>>("api/tasks");
    
    public async Task<List<TaskItemModel>> GetCompletedTasksAsync() =>
        await _http.GetFromJsonAsync<List<TaskItemModel>>("api/tasks/complete") ?? [];

    public async Task<List<TaskItemModel>> GetIncompleteTasksAsync() =>
        await _http.GetFromJsonAsync<List<TaskItemModel>>("api/tasks/incomplete") ?? [];

    public async Task AddTaskAsync(string title, string description)
    {
        var task = new TaskItemModel
        {
            Id = Guid.NewGuid(),
            Title = title,
            Description = description,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow
        };

        await _http.PostAsJsonAsync("api/tasks", task);
    }

    public async Task CompleteTaskAsync(Guid id)
    {
        await _http.PutAsync($"api/tasks/{id}/complete", null);
    }

    public async Task DeleteTaskAsync(Guid id)
    {
        await _http.DeleteAsync($"api/tasks/{id}");
    }
}