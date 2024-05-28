using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManegement.Application.Dtos
{
    public class EmployeeDepartmentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid DepartmentId { get; set; }
        public DateTime JoinedDate { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        
    }
}
