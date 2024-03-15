using Demo.Models;
using Demo.Shared.Enums;

namespace Demo.Business.Projects
{
    internal class ProjectValidator : IValidator
    {
        public DTOStatus Validate<T>(T entity) 
        {
            if (entity is not Project)
                throw new InvalidCastException("Entity is not type of Project");

            Project project = entity as Project;

            DTOStatus status;
            if (project.Planning is null)
                status = DTOStatus.Broken;
            else
                status = DTOStatus.Ok;

            return status;
        }
    }
}
