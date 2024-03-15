using Demo.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Demo.Shared.DTOs;

public class ProjectDTO
{
    public ProjectDTO()
    {
        
    }

    public ProjectDTO(int id, string title, string description, int number) : this()
    {
        Id = id;
        Title = title;
        Description = description;
        Number = number;
    }

    public int Id { get; set; }
    [StringLength(50)]
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int Number { get; set; }

    public ProjectPlanningDTO Planning { get; set; }

    public DTOStatus Status { get; set; }


    public override string ToString()
    {
        return $"{Number} - {Title}";
    }
}
