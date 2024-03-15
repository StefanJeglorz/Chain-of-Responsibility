using Microsoft.EntityFrameworkCore;

namespace Demo.Models
{
    internal class CORDBContext : DbContext, ICORDataLayer
    {
        public CORDBContext(DbContextOptions options) : base(options)
        {
                
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectPlanning> ProjectPlannings { get; set; }
    }
}
