using HydraBusiness;

namespace HydraWeb;

public static class ConfigureBusinessService
{
        public static IServiceCollection AddBusinessServices(this IServiceCollection services){
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<AccountService>();
            return services;
        }

}   
