using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManegement.Entities.Models
{
    public class ProjectEmployee
    {
        [ForeignKey("ProjectId")]
        public Guid ProjectId { get; set; }
        
        [ForeignKey("EmployeeId")]
        public Guid EmployeeId { get; set; }
        [Required]
        public bool Enable {  get; set; }
        public virtual Project? Project { get; set; }
        public virtual Employee? Employee { get; set; }
    }
}
