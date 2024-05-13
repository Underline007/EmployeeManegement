using EmployeeManegement.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManegement.Application.Dtos
{
    public class EmployeeDto
    {
        [Required]
        [MaxLength(30, ErrorMessage = "Max length of name is 30 characters")]
        public string Name { get; set; } = string.Empty;
        public Guid DepartmentId { get; set; }
        public Department? Department { get; set; }
        public DateTime JoinedDate { get; set; }
        public Salary? Salary { get; set; }
        public ICollection<ProjectEmployee>? EmployeeProjects { get; set; }
    }
}
