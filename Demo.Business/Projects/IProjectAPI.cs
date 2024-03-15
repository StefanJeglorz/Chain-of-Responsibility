using Demo.Shared.DTOs;

namespace Demo.Business.Projects
{
    public interface IProjectAPI
    {
        public Task AddBrokenProjectAsync();
        public Task<List<ProjectDTO>> GetProjectsAsync();
        public Task<ProjectDTO> GetProjectAsync(int id);
        public Task RepairAsync(int id);
    }
}
