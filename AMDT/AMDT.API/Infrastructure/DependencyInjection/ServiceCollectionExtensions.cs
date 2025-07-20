using AMDT.API.Interfaces;
using AMDT.API.Repositories;
using AMDT.API.Services;

namespace AMDT.API.Infrastructure.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection repositories)
        {
            repositories.AddScoped<IStatusRepository, StatusRepository>();
            repositories.AddScoped<IRoleTypeRepository, RoleTypeRepository>();
            repositories.AddScoped<IUserDetailsRepository, UserDetailsRepository>();

            return repositories;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IStatusService, StatusService>();
            services.AddScoped<IRoleTypeService, RoleTypeService>();
            services.AddScoped<IUserDetailsService, UserDetailsService>();

            return services;
        }
    }
}
