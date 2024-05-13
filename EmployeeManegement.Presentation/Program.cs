using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using EmployeeManegement.Application.Interfaces;
using EmployeeManegement.Infrastructure.Data;
using EmployeeManegement.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using EmployeeManegement.Presentation.Controllers;
using EmployeeManegement.Entities.Models;

namespace EmployeeManegement.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register the repository implementation for IGenericRepository
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GennericRepository<>));
            

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var projectRepository = services.GetRequiredService<IGenericRepository<Project>>();
                var projectLogger = services.GetRequiredService<ILogger<ProjectController>>();

                var departmentRepository = services.GetRequiredService<IGenericRepository<Department>>();
                var departmentLogger = services.GetRequiredService<ILogger<DepartmentController>>();

                var employeeRepository = services.GetRequiredService<IGenericRepository<Employee>>();
                var employeeLogger = services.GetRequiredService<ILogger<EmployeeController>>();

                var salaryRepository = services.GetRequiredService<IGenericRepository<Salary>>();
                var salaryLogger = services.GetRequiredService<ILogger<SalaryController>>();

                // Inject repository 
                var projectController = new ProjectController(projectRepository, projectLogger);
                var departmentController = new DepartmentController(departmentRepository, departmentLogger);
                var employeeController = new EmployeeController(employeeRepository, employeeLogger);
                var salaryController = new SalaryController(salaryRepository, salaryLogger);
            }

            app.Run();
        }
    }
}
