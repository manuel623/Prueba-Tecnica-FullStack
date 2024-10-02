using Microsoft.EntityFrameworkCore;

namespace TaskManagementApi.Models
{
    public class TaskManagementContext : DbContext
    {
        public TaskManagementContext(DbContextOptions<TaskManagementContext> options)
            : base(options)
        {
        }

        public DbSet<State> States { get; set; }
        public DbSet<AppTask> Tasks { get; set; }
    }
}

