using Demo.Business.ChainHandler.Shared;
using Demo.Models;
using Demo.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Business.Projects
{
    internal class ProjectAPI(ICORDataLayer dataLayer, [FromKeyedServices(nameof(Project))] IValidator validator,
                                                       [FromKeyedServices(Chains.RepairProjectChain)] IChainHandler repairProjectChain) : IProjectAPI
    {
        public async Task AddBrokenProjectAsync()
        {
            var project = new Project()
            {
                Title = "Broken project"
            };
            dataLayer.Projects.Add(project);
            await dataLayer.SaveChangesAsync();
        }

        public async Task<ProjectDTO> GetProjectAsync(int id)
        {
            var project = await dataLayer.Projects.Include(x => x.Planning).FirstOrDefaultAsync(x => x.Id == id);
            if (project == null)
                throw new NullReferenceException("Project not found");

            ProjectDTO dto = new(project.Id, project.Title, project.Description, project.Number);
            if (project.Planning is not null)
            {
                dto.Planning = new()
                {
                    Id = project.Planning.Id,
                    ProjectId = project.Planning.Id,
                    Budget = project.Planning.Budget,
                    From = project.Planning.From,
                    Until = project.Planning.Until,
                };
            }
            dto.Status = validator.Validate(project);

            return dto;
        }

        public async Task<List<ProjectDTO>> GetProjectsAsync()
        {
            var projects = await dataLayer.Projects.ToListAsync();
            return projects.Select(project => new ProjectDTO(project.Id, project.Title, project.Description, project.Number)).ToList();
        }

        public async Task RepairAsync(int id)
        {
            await repairProjectChain.HandleAsync(id);
        }
    }
}
