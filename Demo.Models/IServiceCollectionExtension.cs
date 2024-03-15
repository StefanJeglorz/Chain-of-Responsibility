using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Models
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddModelsServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDbContext<ICORDataLayer, CORDBContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Default"));
            });
        }
    }
}
