using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManegement.Entities.Models
{
    public class Salary
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid EmployeeId { get; set; }
        [Required]
        public decimal EmployeeSalary { get; set; }
        [Required]
        public virtual Employee? Employee { get; set; }
    }
}
