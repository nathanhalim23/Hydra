using HydraDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HydraDataAccess;

public class Dependencies
{
    public static void ConfigureService(IConfiguration configuration, IServiceCollection service){
        service.AddDbContext<HydraContext>(
            option => option.UseSqlServer(configuration.GetConnectionString("HydraConnection"))
        );
    }
}
