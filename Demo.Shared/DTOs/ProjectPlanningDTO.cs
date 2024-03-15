namespace Demo.Shared.DTOs
{
    public class ProjectPlanningDTO
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }

        public DateOnly From { get; set; }
        public DateOnly Until { get; set; }

        public decimal Budget { get; set; }
    }
}
