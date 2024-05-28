using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManegement.Entities.Models;
using EmployeeManegement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection; 

namespace EmployeeManegement.Infrastructure.Extentions
{
    public class DataSeeder
    {
        public static void SeedData(IServiceProvider serviceProvider)
        {
            using (var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>())
            {
                if (!dbContext.Departments.Any())
                {
                    dbContext.Departments.AddRange(
                        new Department()
                        {
                            Id = Guid.NewGuid(),
                            Name= "Test Seed"
                        },
                        new Department()
                        {
                            Id = Guid.NewGuid(),
                            Name = "Software Development"
                        },
                        new Department()
                        {
                            Id = Guid.NewGuid(),
                            Name = "Finance"
                        },
                        new Department()
                        {
                            Id = Guid.NewGuid(),
                            Name = "Accountant"
                        },
                        new Department()
                        {
                            Id = Guid.NewGuid(),
                            Name = "HR"
                        }
                    );
                    dbContext.SaveChanges();
                }

                if (!dbContext.Projects.Any())
                {
                    dbContext.Projects.AddRange(
                        new Project()
                        {
                            Id = Guid.NewGuid(),
                            Name = "Project 1"
                        },
                        new Project()
                        {
                            Id = Guid.NewGuid(),
                            Name = "Project 2"
                        }
                    );
                    dbContext.SaveChanges();
                }
            }
        }
    }
}
