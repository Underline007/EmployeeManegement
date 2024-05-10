using EmployeeManegement.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManegement.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<ProjectEmployee>().HasKey(pe => new { pe.ProjectId, pe.EmployeeId });

            modelBuilder.Entity<Department>().HasData(
                new Department { Id = Guid.NewGuid(), Name = "Software Development" },
                new Department { Id = Guid.NewGuid(), Name = "Finance" },
                new Department { Id = Guid.NewGuid(), Name = "Accountant" },
                new Department { Id = Guid.NewGuid(), Name = "HR" }
            );
        }
    }
}
