namespace HydraWeb;

using Microsoft.AspNetCore.Authentication.Cookies;
using static HydraDataAccess.Dependencies;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        

        IServiceCollection services = builder.Services;
        IConfiguration configuration = builder.Configuration;
        services.AddControllersWithViews();

        ConfigureService(configuration, services);
        services.AddBusinessServices();

         services.AddAuthentication(
            CookieAuthenticationDefaults.AuthenticationScheme
        )
        .AddCookie(options => {
            options.Cookie.Name = "AuthenticationCookie";
            options.LoginPath = "/LoginFail";
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            options.AccessDeniedPath = "/AccessDenied";
        });
        services.AddAuthentication();
        services.AddAuthorization();

        var app = builder.Build();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Account}/{action=LoginPage}"
        );

        app.UseStaticFiles();

        app.Run();
    }
}
