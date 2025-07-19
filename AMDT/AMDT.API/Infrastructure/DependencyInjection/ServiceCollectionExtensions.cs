using AMDT.API.Interfaces;
using AMDT.API.Repositories;

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
            services.AddScoped<IStatusRepository, StatusRepository>();
            services.AddScoped<IRoleTypeRepository, RoleTypeRepository>();
            services.AddScoped<IUserDetailsRepository, UserDetailsRepository>();

            return services;
        }
    }
}
