using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.Models
{
    public class ProjectPlanning
    {
        [Key]
        public int Id{ get; set; }
        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }

        public DateOnly From { get; set; }
        public DateOnly Until { get; set; }
        
        public decimal Budget { get; set; }
    }
}
