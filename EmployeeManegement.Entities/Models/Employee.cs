using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManegement.Entities.Models
{
    public class Employee
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "Max length of name is 30 characters")]
        public string Name { get; set; } = string.Empty;
        [ForeignKey("DepartmentId")]
        public Guid DepartmentId { get; set; }
        
        [Required]
        public DateTime JoinedDate { get; set; }
        [Required]
        public Salary Salary { get; set; }

        public ICollection<ProjectEmployee> EmployeeProjects { get; set; } = new List<ProjectEmployee>();
    }
}
