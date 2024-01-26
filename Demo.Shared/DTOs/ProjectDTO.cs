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
    public string Title { get; set; }
    public string Description { get; set; }
    public int Number { get; set; }
}
