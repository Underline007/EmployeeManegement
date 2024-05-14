using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManegement.Application.Dtos
{
    public class EmployeeProjectDto
    {
        public Guid? EmployeeId { get; set; }
        public string? Name { get; set; }
        public Guid? ProjectId { get; set; }
        public string? ProjectName { get; set; }
    }

}
