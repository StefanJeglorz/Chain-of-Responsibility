using System.Xml;
using Demo.Business.ChainHandler.Shared;
using Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.Business.ChainHandler.Handlers;

internal class CheckAndRepairProjectPlanningHandler(IChainHandler next, ICORDataLayer dbContext)
    : ChainHandlerBase(next)
{
    public override async Task HandleAsync(int id)
    {
        Project project = await dbContext.Projects.Include(x => x.Planning).FirstOrDefaultAsync(x => x.Id == id);
        if (project is not null)
        {
            if (project.Planning is null)
            {
            }

            await base.HandleAsync(id);
        }
    }
}