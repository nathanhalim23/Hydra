using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using static HydraDataAccess.Dependencies;

namespace HydraAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: MyAllowSpecificOrigins,
                            policy  =>
                            {
                                policy.WithOrigins("https://localhost:8080").AllowAnyHeader().AllowAnyMethod();
                            });
        });

        IConfiguration configuration = builder.Configuration;
        IServiceCollection services = builder.Services;

        services.AddControllersWithViews();

        ConfigureService(configuration, services);
        services.AddSwaggerGen();
        services.AddControllers();
        services.AddBusinessServices();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(
                options => {
                    options.TokenValidationParameters = new TokenValidationParameters(){
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value)
                        ),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                }
            );

        services.AddSwaggerGen(options => {
            options.SwaggerDoc("v1", new OpenApiInfo(){Title = "Hydra API"});
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme{
                Description = "Example value = Bearer [token]",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            options.OperationFilter<SecurityRequirementsOperationFilter>();
        });

        var app = builder.Build();

        if(app.Environment.IsDevelopment()){
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.UseCors(MyAllowSpecificOrigins);

        app.Run();
    }
}
