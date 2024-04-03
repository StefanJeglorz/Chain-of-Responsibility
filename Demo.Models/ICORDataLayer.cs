using Microsoft.EntityFrameworkCore;

namespace Demo.Models
{
    public interface ICORDataLayer
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectPlanning> ProjectPlannings { get; set; }


        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
