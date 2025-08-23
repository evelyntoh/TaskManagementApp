using Microsoft.EntityFrameworkCore;
using TaskManagementApp.Domain;
using TaskManagementApp.Domain.Models;

namespace TaskManagementApp.Infrastructure;

public class DatabaseContext: DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
    
    public DbSet<TaskItem> Tasks { get; set; }
}