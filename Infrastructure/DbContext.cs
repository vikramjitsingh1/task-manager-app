using Microsoft.EntityFrameworkCore;
using TmModule.Modules.Tasks.Models;
using TmModule.Modules.Users.Models;
using TmModule.Modules.Status.Models;
using TmModule.Application.CoreController;

namespace TmModule.Infrastructure
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<UsersTable> Users { get; set; }
        public DbSet<TaskTable> Tasks { get; set; }
        public DbSet<StatusTable> Status { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StatusTable>()
                .HasKey(s => new { s.UserId, s.TaskId });

            modelBuilder.Entity<StatusTable>()
                .HasOne(s => s.Users)
                .WithMany()
                .HasForeignKey(s => s.UserId);

            modelBuilder.Entity<StatusTable>()
                .HasOne(s => s.Task)
                .WithMany()
                .HasForeignKey(s => s.TaskId);
        }
    }
}