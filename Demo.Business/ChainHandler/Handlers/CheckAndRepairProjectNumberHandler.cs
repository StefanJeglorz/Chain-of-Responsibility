using Demo.Business.ChainHandler.Shared;
using Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.Business.ChainHandler.Handlers;

internal class CheckAndRepairProjectNumberHandler(IChainHandler next, ICORDataLayer dbContext) : ChainHandlerBase(next)
{
    public override async Task HandleAsync(int id)
    {
        Project project = await dbContext.Projects.FirstOrDefaultAsync(x => x.Id == id);
        if (project is not null)
        {
            if (project.Number == 0)
            {
                project.Number = await dbContext.Projects.MaxAsync(x => x.Number) + 1;
                await dbContext.SaveChangesAsync();
            }
            await base.HandleAsync(id);
        }
    }
}