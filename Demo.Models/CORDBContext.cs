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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(x => x.Planning)
                      .WithOne(x => x.Project)
                      .HasForeignKey<ProjectPlanning>(x => x.ProjectId);
            });
            modelBuilder.Entity<ProjectPlanning>(entity =>
            {
                entity.HasKey(x => x.Id);
            });
            
        }
    }
}
