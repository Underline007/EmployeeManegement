using EmployeeManegement.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManegement.Application.Dtos
{
    public class ProjectDto
    {
        [Required]
        [MaxLength(30, ErrorMessage = "Max length of name is 30 characters")]
        public string Name { get; set; } = string.Empty;
        public List<Guid>? EmployeeIds { get; set; }

    }
}
