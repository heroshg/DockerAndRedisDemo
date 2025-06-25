using DockerAndRedisDemo.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DockerAndRedisDemo
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DockerAndRedisDemoDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration["Redis:Connection"];
            });

            return services;
        }
    }
}
