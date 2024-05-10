using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManegement.Entities.Models
{
    public class Project
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "Max length of name is 30 characters")]
        public string Name { get; set; } = string.Empty;
        public ICollection<ProjectEmployee> ProjectEmployees { get; set; } = new List<ProjectEmployee>();

    }
}
