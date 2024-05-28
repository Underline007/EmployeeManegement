using EmployeeManegement.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManegement.Application.Dtos
{
    public class SalaryDto
    {
        public Guid EmployeeId { get; set; }
        public decimal EmployeeSalary { get; set; }
    }
}
