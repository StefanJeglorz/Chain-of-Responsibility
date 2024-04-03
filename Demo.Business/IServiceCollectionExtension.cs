using Demo.Business.ChainHandler.Handlers;
using Demo.Business.ChainHandler.Shared;
using Demo.Business.Projects;
using Demo.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Business
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IProjectAPI, ProjectAPI>().AddKeyedScoped<IValidator, ProjectValidator>(nameof(Project));

            services.Chain<IChainHandler>(Chains.RepairProjectChain)
                    .Add<CheckAndRepairProjectNumberHandler>()
                    .Add<CheckAndRepairProjectPlanningHandler>()
                    .Configure();

            return services;
        }
    }
}
