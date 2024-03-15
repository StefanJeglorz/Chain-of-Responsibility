using System.ComponentModel.DataAnnotations;

namespace Demo.Models;

public class Project
{
    [Key]
    public int Id { get; set; }
    [StringLength(50)]
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int Number { get; set; }

    public virtual ProjectPlanning Planning { get; set; }

}
