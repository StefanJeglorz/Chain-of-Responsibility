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
                    .Add<Handler1>()
                    .Add<Handler2>()
                    .Add<Handler3>()
                    .Add<Handler4>()
                    .Configure();

            services.Chain<IChainHandler>(Chains.DemoChain2)
                    .Add<Handler3>()
                    .Add<Handler4>()
                    .Add<Handler1>()
                    .Add<Handler2>()
                    .Configure();

            return services;
        }
    }
}
